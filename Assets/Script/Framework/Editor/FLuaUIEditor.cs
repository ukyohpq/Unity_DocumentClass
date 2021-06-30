using System;
using Framework.core.Components;
using Framework.LuaUI.Components;
using Script.Framework.LuaUI.Components;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Framework.Editor
{
    public class FLuaUIEditor
    {
        private static void CommonCreate<T>() where T:GameObjectLuaBinder
        {
            var go = Selection.activeGameObject;
            var binder = go.AddComponent<T>();
            binder.Container = go.transform;
            go.transform.localPosition = Vector3.zero;
        }
        
        [MenuItem("GameObject/FLuaUI/text", false, 1)]
        public static void FLuaUIText()
        {
            EditorApplication.ExecuteMenuItem("GameObject/UI/Text");
            CommonCreate<FLuaText>();
        }

        [MenuItem("GameObject/FLuaUI/Button")]
        public static void FLuaUIButton()
        {
            EditorApplication.ExecuteMenuItem("GameObject/UI/Button");
            CommonCreate<FLuaButton>();
        }
        
        [MenuItem("GameObject/FLuaUI/Image")]
        public static void FLuaUIImage()
        {
            EditorApplication.ExecuteMenuItem("GameObject/UI/Image");
            CommonCreate<FLuaImage>();
        }
        
        [MenuItem("GameObject/FLuaUI/DocumentClass")]
        public static void FLuaUIDocumentClass()
        {
            EditorApplication.ExecuteMenuItem("GameObject/Create Empty");
            CommonCreate<DocumentClass>();
        }
        
        [MenuItem("GameObject/FLuaUI/Scroll View", false, 1)]
        public static void FLuaUIScrollView()
        {
            EditorApplication.ExecuteMenuItem("GameObject/UI/Scroll View");
            var go = Selection.activeGameObject;
            var sr = go.GetComponent<ScrollRect>();
            sr.horizontalScrollbar = null;
            sr.verticalScrollbar = null;
            go.transform.Find("Scrollbar Horizontal").gameObject.SetActive(false);
            go.transform.Find("Scrollbar Vertical").gameObject.SetActive(false);
            var binder = go.AddComponent<GameObjectLuaBinder>();
            var content = go.transform.Find("Viewport/Content");
            binder.Container = content;
            content.gameObject.AddComponent<GridLayoutGroup>();
            go.transform.localPosition = Vector3.zero;
            content.localPosition = Vector3.zero;
        }
    }
}