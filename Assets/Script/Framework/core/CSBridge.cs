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
            docu.SetContextID(contextID);
            
            
//            var luaCall = MainGame.Ins.LuaState.GetFunction("CSCallLua");
//            luaCall.Call("COMPLETE", contextID, go);
        }
    }
}