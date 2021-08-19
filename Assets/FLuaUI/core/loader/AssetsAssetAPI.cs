using System.IO;
using UnityEditor;
using UnityEngine;

namespace FLuaUI.core.loader
{
    public class AssetsAssetAPI
    {
        public Sprite LoadSprite(string path)
        {
#if UNITY_EDITOR && !USE_BUNDLE
            return AssetDatabase.LoadAssetAtPath<Sprite>("Assets/UI/Atlas/" + path);
#else
            var bundleName = path.Substring(0, path.IndexOf("\\")).ToLower();
            var request = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/atlas_" + bundleName);
            if (request.isDone)
            {
//                yield return request;
            }
            throw new System.NotImplementedException();
#endif
        }

        public GameObject LoadPrefab(string path)
        {
#if UNITY_EDITOR && !USE_BUNDLE
            return AssetDatabase.LoadAssetAtPath<GameObject>(path);
#else
                throw new System.NotImplementedException();
#endif
        }

        public Object[] LoadAtlas(string atlas)
        {
#if UNITY_EDITOR && !USE_BUNDLE
            return AssetDatabase.LoadAllAssetsAtPath(atlas);
#else
                throw new System.NotImplementedException();
#endif
        }
    }
}