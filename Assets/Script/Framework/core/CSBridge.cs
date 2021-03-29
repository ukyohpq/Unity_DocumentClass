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
        private static List<LoaderContext> loaderContexts = new List<LoaderContext>();
        private static Dictionary<string, AssetPrototype> AssetDict = new Dictionary<string, AssetPrototype>();
        private static Dictionary<int, GameObject> contextPrefabDic = new Dictionary<int, GameObject>();
        public static void LoadPrefab(string path, int contextID)
        {
            loaderContexts.Add(new LoaderContext(path, contextID));
        }

        [NoToLua]
        public static void LoadAsset()
        {
            for (int i = 0; i < 10; i++)
            {
                if (loaderContexts.Count == 0) return;
                var context = loaderContexts[0];
                loaderContexts.RemoveAt(0);
                var path = context.Path;
                var contextID = context.ContextId;
                AssetPrototype asset;
                if (!AssetDict.TryGetValue(path , out asset))
                {
                    var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                    asset = new AssetPrototype(prefab);
                    AssetDict.Add(path, asset);
                }
                
                if (asset == null)
                {
                    BTLog.Error("can not find prefab in path:{0}", path);
                    return;
                }
                var go = asset.GetGameObject();
                MainGame.Ins.AddChild2Stage(go);
            
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
                docu.SetContextId(contextID);
            }
            
        } 
        public static void AddChildTo(GameObject child, GameObject parent)
        {
            
        }
    }
}