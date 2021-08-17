using UnityEditor;
using UnityEngine;

namespace FLuaUI.core.loader
{
    public class EditorAssetsAPI:IAssetsAPI
    {
        public Sprite LoadSprite(string path)
        {
            return AssetDatabase.LoadAssetAtPath<Sprite>(path);
        }

        public GameObject LoadPrefab(string path)
        {
            return AssetDatabase.LoadAssetAtPath<GameObject>(path);
        }

        public Object[] LoadAtlas(string atlas)
        {
            return AssetDatabase.LoadAllAssetsAtPath(atlas);
        }
    }
}