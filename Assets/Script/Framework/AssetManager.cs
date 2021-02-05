using Babeltime.Log;
using UnityEditor;
using UnityEngine;

namespace Framework
{
    public class AssetManager
    {
        private struct Request
        {
            public int Id;
        }
        
        public static void Load(int requestId, string path)
        {
#if LOADFROM_BUNDLE || !UNITY_EDITOR || UIFROMBUNNDLE
#else
            var go = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (go == null)
            {
                BTLog.Error("load asset failed. requestId:{} path:{}", requestId, path);
                return;
            }

            GameObject.Instantiate(go);
#endif
            
        }
    }
}