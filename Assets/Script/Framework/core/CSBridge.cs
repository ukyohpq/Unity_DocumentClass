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
        public static void LoadPrefab(string path, LuaTable contextID)
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
                var luaTable = context.LuaTb;
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
                
                var docu = go.GetComponent<DocumentClass>();
                if (docu == null)
                {
                    throw new Exception("must has Component DocumentClass!");
                }
                docu.SetLuaTable(luaTable);
            }
            
        }
    }
}