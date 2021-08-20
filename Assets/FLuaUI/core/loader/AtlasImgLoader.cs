using System.Collections;
using Babeltime.Log;
using LuaInterface;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace FLuaUI.core.loader
{
    public class AtlasImgLoader:BaseLoader
    {
        private static int c = 0;
        private string atlas;
        private string name;
        private int flag;
        public AtlasImgLoader(LuaTable lt, string atlas, string name):base(lt)
        {
            this.atlas = atlas;
            this.name = name;
            this.flag = c++;
        }

        public override IEnumerator Load()
        {
#if UNITY_EDITOR && !USE_BUNDLE
            var texs = AssetDatabase.LoadAllAssetsAtPath("Assets/UI/Atlas/" + atlas);
#else
            Sprite[] texs = null;
//            BTLog.Error("atlas:{0} name:{1}", atlas, name);
            if (!AssetsManager.Atlases.TryGetValue(this.atlas, out texs))
            {
                var tempPath = atlas.Replace("\\", "/");
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
                    AssetsManager.Bundles["atlas_" + bundleName] = ab;
                }
                texs = ab.LoadAssetWithSubAssets<Sprite>(assetName);
                AssetsManager.Atlases[this.atlas] = texs;
            }
#endif
            if (texs == null)
            {
                BTLog.Error("can not find atlas:{0}", atlas);
                yield break;
            }

            Sprite t = null;
            for (int i = 0; i < texs.Length; i++)
            {
                if (texs[i].name == name)
                {
                    t = texs[i] as Sprite;
                }
            }

            if (t == null)
            {
                BTLog.Error("can not find sprite:{0} in atlas:{1}", name, atlas);
                yield break;
            }
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
            
            var binder = ls.ToVariant(-1) as MonoBehaviour;
            var unityImage = binder.gameObject.GetComponent<Image>(); 
            unityImage.sprite = t;
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