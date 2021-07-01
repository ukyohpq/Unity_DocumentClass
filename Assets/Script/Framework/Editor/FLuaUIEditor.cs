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
        private static void CommonCreate<T>(string menuPath) where T:GameObjectLuaBinder
        {
            var parent = Selection.activeGameObject;
            EditorApplication.ExecuteMenuItem(menuPath);
            var go = Selection.activeGameObject;
            if (parent != null)
            {
                go.transform.parent = parent.transform;
                var canvas = go.transform.root.Find("Canvas");
                if (canvas != null)
                {
                    canvas.parent = null;
                    GameObject.Destroy(canvas.gameObject);
                }
            }
            
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
            CommonCreate<FLuaText>("GameObject/UI/Text");
        }

        [MenuItem("GameObject/FLuaUI/Button")]
        public static void FLuaUIButton()
        {
            CommonCreate<FLuaButton>("GameObject/UI/Button");
        }
        
        [MenuItem("GameObject/FLuaUI/Image")]
        public static void FLuaUIImage()
        {
            CommonCreate<FLuaImage>("GameObject/UI/Image");
        }
        
        [MenuItem("GameObject/FLuaUI/DocumentClass")]
        public static void FLuaUIDocumentClass()
        {
            CommonCreate<DocumentClass>("GameObject/Create Empty");
        }
        
        [MenuItem("GameObject/FLuaUI/Scroll View", false, 1)]
        public static void FLuaUIScrollView()
        {
            CommonCreate<FLuaScrollView>("GameObject/UI/Scroll View");
            var go = Selection.activeGameObject;
            var sr = go.GetComponent<ScrollRect>();
            sr.horizontalScrollbar = null;
            sr.verticalScrollbar = null;
            go.transform.Find("Scrollbar Horizontal").gameObject.SetActive(false);
            go.transform.Find("Scrollbar Vertical").gameObject.SetActive(false);
            var binder = go.GetComponent<FLuaScrollView>();
            var content = go.transform.Find("Viewport/Content");
            binder.Container = content;
            content.gameObject.AddComponent<GridLayoutGroup>();
            content.localPosition = Vector3.zero;
        }

        [MenuItem("GameObject/FLuaUI/Toggle Group", false, 1)]
        public static void FLuaUIToggleGroup()
        {
            CommonCreate<FLuaToggleGroup>("GameObject/Create Empty");
            var obj = Selection.activeGameObject;
            obj.AddComponent<ToggleGroup>();
            obj.name = "ToggleGroup";
        }
        
        [MenuItem("GameObject/FLuaUI/Toggle", false, 1)]
        public static void FLuaUIToggle()
        {
            CommonCreate<FLuaToggle>("GameObject/UI/Toggle");
            var toggle = Selection.activeGameObject;
            var parentGroup = toggle.transform.parent.GetComponent<ToggleGroup>();
            if (parentGroup != null)
            {
                toggle.GetComponent<Toggle>().group = parentGroup;
            }
        }
    }
}