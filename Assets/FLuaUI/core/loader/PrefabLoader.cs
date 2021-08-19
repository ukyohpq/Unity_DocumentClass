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
        public PrefabLoader(LuaTable lt):base(lt)
        {
            
        }
        public override IEnumerator Load()
        {
            var fc = lt["GetAssetPath"] as LuaFunction;
            var path = fc.Invoke<string>();
#if UNITY_EDITOR && !USE_BUNDLE
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/UI/Prefab/" + path);
#else
            path = path.Replace("Assets/UI/Prefab/", "");
            var bundleName = path.Substring(0, path.IndexOf("/")).ToLower();
            var goName = path.Substring(path.LastIndexOf("/") + 1);
            goName = goName.Replace(".prefab", "");
            var request = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/ui_" + bundleName);
            yield return request;
            var prefabs = request.assetBundle.LoadAllAssets<GameObject>();
            GameObject prefab = null;
            foreach (var p in prefabs)
            {
                if (p.name == goName)
                {
                    prefab = p;
                    break;
                }
            }
#endif
            
            if (prefab == null)
            {
                BTLog.Error("can not find prefab in path:{0}", path);
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