using System;
using System.Collections.Generic;
using Babeltime.Log;
using LuaInterface;
using UnityEditor;
using UnityEngine;

namespace Framework.core
{
    public class CSBridge
    {
        private static Dictionary<int, GameObject> contextPrefabDic = new Dictionary<int, GameObject>();
        public static void LoadPrefab(string path, int contextID)
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab == null)
            {
                BTLog.Error("can not find prefab in path:{0}", path);
                return;
            }
            var go = GameObject.Instantiate(prefab);
//            TODO  UIRoot不能在这里写死。考虑引入UIStage
            go.transform.parent = GameObject.Find("UIRoot").transform;
            go.transform.localPosition = Vector3.zero;
            var luaCall = LuaState.Get(new IntPtr()).GetFunction("CSCallLua");
            if (contextPrefabDic.ContainsKey(contextID))
            {
                BTLog.Error("contextPrefabDic contains key:{0}", contextID);
                return;
            }else if (contextPrefabDic.ContainsValue(go))
            {
                BTLog.Error("contextPrefabDic contains value:{0}", go.name);
                return;
            }
            
            contextPrefabDic[contextID] = go;
            var ls = LuaState.Get(new IntPtr());
            var d = ls.GetTable("prefabIDMap")[contextID] as LuaTable;

            var children = go.transform.GetComponentsInChildren<Transform>();
            d["gameobject"] = go;
            foreach (var trans in children)
            {
                BTLog.Error("child name:{0}", trans.name);
                var name = trans.name;
                if (name == null) continue;
                if (name.Substring(0, 2) != "m_") continue;
                var suffix = name.Substring(2);
                Type T;
                switch (suffix)
                {
                    case "Text":
                        T = typeof(UnityEngine.UI.Text);
                        break;
                    case "Button":
                        T = typeof(Framework.UI.Button);
                        break;
                    default:
                        BTLog.Warning("未定义的后缀名");
                        continue;
                }
                
                d[trans.name] = trans.GetComponent(T);
            }

            luaCall.Call("COMPLETE", contextID, go);
            
        }
    }
}