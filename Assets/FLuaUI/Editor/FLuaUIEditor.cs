using FLuaUI.Components;
using FLuaUI.LuaUI.Components;
using Script.Framework.LuaUI.Components;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace FLuaUI.Editor
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
        
        [MenuItem("GameObject/FLuaUI/ExportFLua", false, 1)]
        private static void FLuaUIExport()
        {
            AssetDatabase.ExportPackage(new []
            {
                "Assets/codeandweb.com/Editor",
                "Assets/Editor",
                "Assets/FLuaUI",
                "Assets/Lua",
                "Assets/LuaAPI",
                "Assets/Plugins",
                "Assets/Source",
                "Assets/ToLua"
            }, "Assets/FLuaUI.unitypackage", ExportPackageOptions.Interactive | ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies);
        }
        
        [MenuItem("GameObject/FLuaUI/Make scene", false, 2)]
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
        
        [MenuItem("GameObject/FLuaUI/Button")]
        public static void FLuaUIButton()
        {
            CommonCreate<FLuaButton>("GameObject/UI/Button");
        }

        [MenuItem("GameObject/FLuaUI/DocumentClass")]
        public static void FLuaUIDocumentClass()
        {
            CommonCreate<DocumentClass>("GameObject/Create Empty");
        }
        
        [MenuItem("GameObject/FLuaUI/Image")]
        public static void FLuaUIImage()
        {
            CommonCreate<FLuaImage>("GameObject/UI/Image");
        }

        [MenuItem("GameObject/FLuaUI/Input Field")]
        public static void FLuaUIInput()
        {
            CommonCreate<FLuaInputField>("GameObject/UI/Input Field");
        }
        
        [MenuItem("GameObject/FLuaUI/Scroll View")]
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
            var content = go.transform.Find("Viewport/Content") as RectTransform;
            binder.Container = content;
            content.gameObject.AddComponent<GridLayoutGroup>();
            var fitter = content.gameObject.AddComponent<ContentSizeFitter>();
            fitter.horizontalFit = ContentSizeFitter.FitMode.MinSize;
            fitter.verticalFit = ContentSizeFitter.FitMode.MinSize;
            content.localPosition = Vector3.zero;
        }
        
        [MenuItem("GameObject/FLuaUI/Scroll View Panel")]
        public static void FLuaUIScrollViewPanel()
        {
            CommonCreate<FLuaScrollViewPanel>("GameObject/UI/Scroll View");
            var go = Selection.activeGameObject;
            go.name = "Scroll View Panel";
            var sr = go.GetComponent<ScrollRect>();
            sr.horizontalScrollbar = null;
            sr.verticalScrollbar = null;
            sr.movementType = ScrollRect.MovementType.Clamped;
            UnityEngine.Object.DestroyImmediate(go.transform.Find("Scrollbar Horizontal").gameObject);
            UnityEngine.Object.DestroyImmediate(go.transform.Find("Scrollbar Vertical").gameObject);
            var binder = go.GetComponent<FLuaScrollViewPanel>();
            var content = go.transform.Find("Viewport/Content") as RectTransform;
            binder.Container = content;
            content.gameObject.AddComponent<GridLayoutGroup>();
            content.localPosition = Vector3.zero;
        }

        [MenuItem("GameObject/FLuaUI/Text")]
        public static void FLuaUIText()
        {
            CommonCreate<FLuaText>("GameObject/UI/Text");
        }
        
        [MenuItem("GameObject/FLuaUI/Toggle")]
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
        
        [MenuItem("GameObject/FLuaUI/Toggle Group")]
        public static void FLuaUIToggleGroup()
        {
            CommonCreate<FLuaToggleGroup>("GameObject/Create Empty");
            var obj = Selection.activeGameObject;
            obj.AddComponent<ToggleGroup>();
            obj.name = "ToggleGroup";
        }

    }
}