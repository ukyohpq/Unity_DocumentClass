using System;
using System.Collections;
using Babeltime.Log;
using FLuaUI.LuaUI.Components;
using LuaInterface;
using UnityEditor;
using UnityEngine;

namespace FLuaUI.core.loader
{
    public class PrefabLoader:BaseLoader
    {
        private static int c = 0;
        private int flag;
        public PrefabLoader(LuaTable lt):base(lt)
        {
            flag = c++;
        }
        public override IEnumerator Load()
        {
            var fc = lt["GetAssetPath"] as LuaFunction;
            var originPath = fc.Invoke<string>();
#if UNITY_EDITOR && !USE_BUNDLE
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(originPath);
#else
            GameObject prefab = null;
            if (!AssetsManager.Prefabs.TryGetValue(originPath, out prefab))
            {
                var path = originPath.Replace("Assets/UI/Prefab/", "");
                var bundleName = "ui_" + path.Substring(0, path.IndexOf("/")).ToLower();
                AssetBundle ab = null;
                if(!AssetsManager.Bundles.TryGetValue(bundleName, out ab))
                {
                    AssetBundleCreateRequest request = null;
                    if (!AssetsManager.Requests.TryGetValue(bundleName, out request))
                    {
                        request = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/" + bundleName);
                        AssetsManager.Requests[bundleName] = request;
                    }
                    while (!request.isDone)
                    {
                        yield return null;
                    }
                    AssetsManager.Requests.Remove(bundleName);
                    ab = request.assetBundle;
                    if (ab == null)
                    {
                        BTLog.Warning("can not load bundle:{0}", bundleName);
                        yield break;
                    }

                    AssetsManager.Bundles[bundleName] = ab;
                }
                var goName = path.Substring(path.LastIndexOf("/") + 1);
                goName = goName.Replace(".prefab", "");
                
                prefab = ab.LoadAsset<GameObject>(goName);
                AssetsManager.Prefabs[originPath] = prefab;
            }
            
#endif
            
            if (prefab == null)
            {
                BTLog.Error("can not find prefab in path:{0}", originPath);
                yield break;
            }

            var go = GameObject.Instantiate(prefab);
            MainGame.Ins.AddChild2Stage(go);
            go.transform.localPosition = Vector3.zero;
                
            var docu = go.GetComponent<DocumentClass>();
            if (docu == null)
            {
                throw new Exception("must has Component DocumentClass!");
            }
            docu.SetLuaTable(lt);
        }
    }
}