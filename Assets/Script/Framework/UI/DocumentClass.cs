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

        private int contextId = -1;

        [NoToLua]
        public void SetContextId(int value)
        {
            if (contextId == value)
            {
                return;
            }
            contextId = value;
            var prefabLua = MainGame.Ins.GetPrefabLua(contextId);
            if (prefabLua == null)
            {
                BTLog.Error(string.Format("load prefab document but can not find contextID:{0}", contextId));
                return;
            }
            BindLuaClass(prefabLua);
        }

        public int GetContextId()
        {
            return contextId;
        }

        public string GetLuaClassName()
        {
            return LuaClass;
        }
        
        private void BindLuaClass(LuaTable prefabLua)
        {
//            TODO 这里考虑有没有必要把这个gameobject传给lua
            prefabLua["gameObject"] = gameObject;
            BindFieldsOnTrans(transform, prefabLua);
//            完成绑定之后，广播complete事件
            var dispatchMessage = prefabLua["DispatchMessage"] as LuaFunction;
            if (dispatchMessage == null)
            {
                BTLog.Warning("Prefab Lua must has Method:DispatchMessage");
                return;
            }
            else
            {
                dispatchMessage.Call(prefabLua, "COMPLETE");
            }
        }

        private void BindFieldsOnTrans(Transform trans, LuaTable prefabLua)
        {
            BTLog.Debug("bindFiledsOnTrans trans:{0}", trans.name);
            var numChildren = trans.childCount;
            for (int i = 0; i < numChildren; i++)
            {
                var child = trans.GetChild(i);
                var childName = child.name;
                if (childName == "") continue;
                var suffix = Utils.GetSuffixOfGoName(childName);
//                如果不是合法后缀，则直接进入下一级，检测子go有没有需要绑定的
                if (!Utils.IsValidSuffix(suffix))
                {
                    BindFieldsOnTrans(child, prefabLua);
                    continue;
                }
//                对Doc进行特殊处理，这个不能直接绑定cs组件，需要创建一个lua对象，然后进行绑定
                if (suffix == "_Doc")
                {
                    var childDoc = child.GetComponent<DocumentClass>();
                    childDoc.CreatePrefabAndBindLuaClass();
                    var childContextId = childDoc.GetContextId();
                    var childPrefab = MainGame.Ins.GetPrefabLua(childContextId);
                    prefabLua[childName] = childPrefab;
                }
                else
                {
                    BindFieldsOnTrans(child, prefabLua);
                    var T = Utils.GetTypeByComponentSuffix(suffix);
                    if (T == null) continue;
                    prefabLua[childName] = child.GetComponent(T);
                }
                BTLog.Debug("bind {0}. name:{1} childName:{2}", suffix, trans.name, childName);

            }
        }
//        TODO 目前暂时确定父类一定是Framework.UI.Prefab类，不使用自定义父类，因为检测自定义父类是从Framework.UI.Prefab继承而来，比较麻烦，而且考虑使用状态而不是继承来重用Prefab
//        [SerializeField]
//        private string SuperClass = "";
        // Start is called before the first frame update
        void Start()
        {
            if (contextId == -1)
            {
                CreatePrefabAndBindLuaClass();
            }
//            BTLog.Error("documentclass contextID:{0}", contextID);
        }

//        通过在cs端创建lua的Prefab对象进行绑定
        private void CreatePrefabAndBindLuaClass()
        {
            var getPrefabId = MainGame.Ins.LuaState.GetFunction("getPrefabID");
            if (getPrefabId == null)
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
            contextId = getPrefabId.Invoke<LuaTable, int>(prefab);
            BindLuaClass(prefab);
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

