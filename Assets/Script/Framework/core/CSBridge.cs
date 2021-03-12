using System;
using System.Collections.Generic;
using Babeltime.Log;
using Framework.UI;
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
            var docu = go.GetComponent<DocumentClass>();
            if (docu == null)
            {
                throw new Exception("must has Component DocumentClass!");
            }
            docu.ContextId = contextID;
            
            var prefabLua = MainGame.Ins.GetPrefabLua(contextID);
            if (prefabLua == null)
            {
                BTLog.Error(string.Format("load prefab document but can not find contextID:{0}", contextID));
                return;
            }
            var children = go.transform.GetComponentsInChildren<Transform>();
            prefabLua["gameobject"] = go;
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
//            var luaCall = MainGame.Ins.LuaState.GetFunction("CSCallLua");
//            luaCall.Call("COMPLETE", contextID, go);
        }
    }
}