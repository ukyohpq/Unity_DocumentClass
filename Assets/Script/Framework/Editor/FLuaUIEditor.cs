using System;
using Framework.core.Components;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Framework.Editor
{
    public class FLuaUIEditor
    {
        [MenuItem("GameObject/FLuaUI/text", false, 1)]
        public static void FLuaUIText()
        {
            EditorApplication.ExecuteMenuItem("GameObject/UI/Text");
            var go = Selection.activeGameObject;
            go.name = "m_text";
            var binder = go.AddComponent<GameObjectLuaBinder>();
            binder.Container = go.transform;
            go.transform.localPosition = Vector3.zero;
        }
        
        [MenuItem("GameObject/FLuaUI/Scroll View", false, 1)]
        public static void FLuaUIScrollView()
        {
            EditorApplication.ExecuteMenuItem("GameObject/UI/Scroll View");
            var go = Selection.activeGameObject;
            go.name = "m_SV";
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