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

        private void bindLuaClass(LuaTable prefabLua)
        {
            var children = gameObject.transform.GetComponentsInChildren<Transform>();
            prefabLua["gameobject"] = gameObject;
            foreach (var trans in children)
            {
                BTLog.Error("child name:{0}", trans.name);
                var name = trans.name;
                if (name == null) continue;
                if (name.Substring(0, 2) != "m_") continue;
                var suffix = name.Substring(2);
                var T = Utils.GetTypeByComponentSuffix(suffix);
                if (T == null) continue;
                prefabLua[trans.name] = trans.GetComponent(T);
            }

            (prefabLua["DispatchMessage"] as LuaFunction).Call(prefabLua, "Complete");
        }
//        TODO 目前暂时确定父类一定是Framework.UI.Prefab类，不使用自定义父类，因为检测自定义父类是从Framework.UI.Prefab继承而来，比较麻烦，而且考虑使用状态而不是继承来重用Prefab
//        [SerializeField]
//        private string SuperClass = "";
        // Start is called before the first frame update
        void Start()
        {
            if (contextID == -1)
            {
                var getPrefabID = MainGame.Ins.LuaState.GetFunction("getPrefabID");
                if (getPrefabID == null)
                {
                    BTLog.Error("can not find lua function getPrefabID");
                    return;
                }
                var prefabClass = MainGame.Ins.LuaState.GetTable(LuaClass);
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
            BTLog.Error("documentclass contextID:{0}", contextID);
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

