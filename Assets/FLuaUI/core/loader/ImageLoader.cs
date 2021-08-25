using System.Collections;
using System.Collections.Generic;
using Babeltime.Log;
using FLuaUI.LuaUI.Components;
using LuaInterface;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace FLuaUI.core.loader
{
    public class ImageLoader:BaseLoader
    {
        private string path;
        public ImageLoader(LuaTable lt, string path):base(lt)
        {
            this.path = path;
        }
        public override IEnumerator Load()
        {
#if UNITY_EDITOR && !USE_BUNDLE
            var t = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/UI/Atlas/" + path);
#else
            Sprite t = null;
//            BTLog.Error("path:{0}",path);
            if (!AssetsManager.Sprites.TryGetValue(this.path, out t))
            {
                var tempPath = path.Replace("\\", "/");
//                BTLog.Error("tempPath:{0}", tempPath);
                var bundleName = "atlas_" + tempPath.Substring(0, tempPath.IndexOf("/")).ToLower();
//                BTLog.Error("bundleName:{0}", bundleName);
                var assetName = tempPath.Substring(tempPath.LastIndexOf("/") + 1);
                AssetBundle ab = null;
                if (!AssetsManager.Bundles.TryGetValue(bundleName, out ab))
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
                        BTLog.Warning("can not find bundle:{0}", bundleName);
                        yield break;
                    }

                    AssetsManager.Bundles[bundleName] = ab;
                }
                t = ab.LoadAsset<Sprite>(assetName);
                AssetsManager.Sprites[this.path] = t;
            }
            
            
#endif
            var ls = lt.GetLuaState();
            lt.Push();
//            这里使用协程。由于prefab加载也使用了协程，而prefab协程早于该协程创建，所以在调度时，该协程早于prefab协程，导致prefab还没有bind，这里就尝试去通过luatable获取binder，就会获取失败。这里保证在bind之后获取binder
            while (true)
            {
                ls.LuaPushValue(-1);
                ls.LuaGetTable(LuaIndexes.LUA_REGISTRYINDEX);
                if (ls.lua_isnil(-1))
                {
                    ls.LuaPop(1);
                    yield return null;
                }
                else
                {
                    break; 
                }
            }
            var binder = ls.ToVariant(-1) as FLuaImage;
            var unityImage = binder.gameObject.GetComponent<Image>();
            unityImage.sprite = t as Sprite;
            unityImage.SetNativeSize();
            if (binder.AutoNativeSize)
            {
                unityImage.SetNativeSize();
            }
            ls.LuaPop(1);
            ls.LuaGetField(-1, "DispatchMessage");
            if (ls.LuaIsNil(-1))
            {
                ls.LuaPop(1);
                BTLog.Warning("Prefab Lua must has Method:DispatchMessage");
                yield break;
            }
            ls.LuaInsert(-2);
            ls.Push("COMPLETE");
            ls.LuaSafeCall(2, 0, 0, 0);
        }
    }
}