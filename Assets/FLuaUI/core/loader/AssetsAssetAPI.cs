using System.IO;
using UnityEditor;
using UnityEngine;

namespace FLuaUI.core.loader
{
    public class AssetsAssetAPI:IAssetsAPI
    {
        public Sprite LoadSprite(string path)
        {
#if UNITY_EDITOR && !USE_BUNDLE
            return AssetDatabase.LoadAssetAtPath<Sprite>(path);
#else
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