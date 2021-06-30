using System;
using Babeltime.Log;
using Framework.core.Components;
using Framework.LuaUI.Components;
using Script.Framework.LuaUI.Components;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        
        [MenuItem("GameObject/FLuaUI/make scene", false, 1)]
//        TODO 不知道怎么在当前目录下创建场景，就只能先手动创建场景，再代码添加gameobject和component
        public static void FLuaUImakeScene()
        {
            EditorApplication.ExecuteMenuItem("GameObject/UI/Canvas");
            Selection.activeGameObject.name = "UIRoot";
            EditorApplication.ExecuteMenuItem("GameObject/Create Empty");
            var main = Selection.activeGameObject;
            main.name = "MainGame";
            var cmp = main.AddComponent<MainGame>();
            var scene = EditorSceneManager.GetActiveScene();
            var path = scene.path;
            var luaIndex = path.IndexOf("/Lua/");
            if (luaIndex != -1)
            {
                path = path.Substring(luaIndex + 5);
            }
            path = path.Replace(scene.name + ".unity", "");
            path = path.Replace("/", ".");
            path += "Logic";
            cmp.luaLogicPath = path;
            EditorSceneManager.SaveOpenScenes();
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
            var parent = Selection.activeGameObject;
            EditorApplication.ExecuteMenuItem("GameObject/Create Empty");
            var go = Selection.activeGameObject;
            if (parent != null)
            {
                go.transform.parent = parent.transform;
            }
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