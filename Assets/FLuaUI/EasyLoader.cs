using UnityEditor;
using UnityEngine;

namespace FLuaUI
{
    public class EasyLoader
    {
        public static void LoadAsset(string path)
        {
            var go = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (go == null)
            {
                return;
            }

            GameObject.Instantiate(go);
        }
    }
}