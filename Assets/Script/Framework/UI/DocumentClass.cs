using System;
using System.Collections;
using System.Collections.Generic;
using Babeltime.Log;
using LuaInterface;
using UnityEditor;
using UnityEngine;
using Framework.core;

namespace Framework.UI
{
    public class DocumentClass : MonoBehaviour
    {
        [SerializeField]
        private string LuaClass = "";

        private int contextID = -1;

        [NoToLua]
        public void SetContextID(int value)
        {
            if (contextID == value)
            {
                return;
            }
            contextID = value;
            var prefabLua = MainGame.Ins.GetPrefabLua(contextID);
            if (prefabLua == null)
            {
                BTLog.Error(string.Format("load prefab document but can not find contextID:{0}", contextID));
                return;
            }
            bindLuaClass(prefabLua);
        }

        public int GetContextID()
        {
            return contextID;
        }

        public string GetLuaClassName()
        {
            return LuaClass;
        }
        
        private void bindLuaClass(LuaTable prefabLua)
        {
//            TODO 这里考虑有没有必要把这个gameobject传给lua
            prefabLua["gameObject"] = gameObject;
            bindFiledsOnTrans(transform, prefabLua);
        }

        private void bindFiledsOnTrans(Transform trans, LuaTable prefabLua)
        {
            BTLog.Error("bindFiledsOnTrans trans:{0}", trans.name);
            var numChildren = trans.childCount;
            for (int i = 0; i < numChildren; i++)
            {
                var child = trans.GetChild(i);
                var name = child.name;
                if (name == null) continue;
//                如果不是合法后缀，则直接进入下一级，检测子go有没有需要绑定的
                if (!isValidSuffix(name))
                {
                    bindFiledsOnTrans(child, prefabLua);
                    continue;
                }
                var suffix = getSuffix(name);
//                对Doc进行特殊处理，这个不能直接绑定cs组件，需要创建一个lua对象，然后进行绑定
                if (suffix == "_Doc")
                {
                    var childDoc = child.GetComponent<DocumentClass>();
                    childDoc.BindSelf();
                    var contextID = childDoc.GetContextID();
                    var childPrefab = MainGame.Ins.GetPrefabLua(contextID);
                    prefabLua[name] = childPrefab;
                }
                else
                {
                    bindFiledsOnTrans(child, prefabLua);
                    var T = Utils.GetTypeByComponentSuffix(suffix);
                    if (T == null) continue;
                    prefabLua[name] = child.GetComponent(T);
                }
                BTLog.Error("bind {0}. name:{1} childName:{2}", suffix, trans.name, name);

            }

            (prefabLua["DispatchMessage"] as LuaFunction).Call(prefabLua, "Complete");
        }
//        TODO 目前暂时确定父类一定是Framework.UI.Prefab类，不使用自定义父类，因为检测自定义父类是从Framework.UI.Prefab继承而来，比较麻烦，而且考虑使用状态而不是继承来重用Prefab
//        [SerializeField]
//        private string SuperClass = "";
        // Start is called before the first frame update
        void Start()
        {
            BTLog.Error("===============start:{0}", name);
            if (contextID == -1)
            {
                BindSelf();
            }
            BTLog.Error("documentclass contextID:{0}", contextID);
        }

        private void BindSelf()
        {
            var getPrefabID = MainGame.Ins.LuaState.GetFunction("getPrefabID");
            if (getPrefabID == null)
            {
                BTLog.Error("can not find lua function getPrefabID");
                return;
            }

            var className = Utils.MakeClassName(LuaClass);
            var prefabClass = MainGame.Ins.LuaState.GetTable(className);
            if (prefabClass == null)
            {
                BTLog.Error("can not find lua class:{0}", LuaClass);
                return;
            }

            var constructor = prefabClass["New"] as LuaFunction;
            if (constructor == null)
            {
                BTLog.Error("can not find constructor for lua class:{0}", LuaClass);
                return;
            }
            var prefab = constructor.Invoke<LuaTable>();
            contextID = getPrefabID.Invoke<LuaTable, int>(prefab);
            bindLuaClass(prefab);
        }
        private string getSuffix(string goName)
        {
            var index = goName.LastIndexOf("_");
            if (index == -1)
            {
                return "";
            }
            return goName.Substring(index);
        }
        private bool isValidSuffix(string goName)
        {
            var suffix = getSuffix(goName);
            BTLog.Error("==========suffix:{0}", suffix);
            return Utils.IsValidSuffix(suffix);
        }
        // Update is called once per frame
        void Update()
        {
            
        }
        
        private void OnDestroy()
        {
            
        }
    }
}

