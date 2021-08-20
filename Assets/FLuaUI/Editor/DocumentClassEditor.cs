using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Babeltime.Log;
using FLuaUI.Components;
using FLuaUI.core;
using FLuaUI.LuaUI.Components;
using UnityEditor;
using UnityEngine;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;

namespace FLuaUI.Editor
{
    [CustomEditor(typeof(DocumentClass))]
    public class DocumentClassEditor:UnityEditor.Editor
    {
        private SerializedProperty m_LuaClass;
        private SerializedProperty m_SuperClass;

        private List<string[]> _fields;
        private const string UI_BEGIN_CODE = "----------------------------- 以下为 UI代码 不可修改 -----------------------------------";
        private const string UI_END_CODE = "----------------------------- 以下为 逻辑代码 可以修改 -----------------------------------";

        private const string SUPER_CLASS_TEMPLATE = @"local super = require(""Framework.UI.Prefab"")

---@class {0}:Framework.UI.Prefab
CommonPrefab = class(""{0}"", Prefab)

function {1}:ctor(autobind)
    super.ctor(self, autobind)
end

return {1}";
        private void OnEnable()
        {
            m_LuaClass = serializedObject.FindProperty("LuaClass");
            m_SuperClass = serializedObject.FindProperty("SuperClass");
            if (m_LuaClass.stringValue == "")
            {
                var str = EditorSceneManager.GetActiveScene().path;
                str = str.Replace(".unity", "");
                str = str.Replace("/", ".");
                m_LuaClass.stringValue = str;
            }
            serializedObject.ApplyModifiedProperties();
        }

        public override void OnInspectorGUI()
        {
//            ReadMe Title
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Unity文档类");
            EditorGUILayout.EndHorizontal();
//            ReadMe
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(@"要从Prefab上导出组件，需要遵守如下命名规范:
按钮: _Button
文本: _Text
文档类: _Doc
");
            EditorGUILayout.EndHorizontal();
//            SuperClassName
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(m_SuperClass, new GUIContent("SuperClass"));
            EditorGUILayout.EndHorizontal();
//            LuaClassName
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(m_LuaClass, new GUIContent("LuaClass"));
            EditorGUILayout.EndHorizontal();

            
//            saveAndCreate
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("save"))
            {
                saveAndCreate();
            }
            EditorGUILayout.EndHorizontal();
            
            serializedObject.ApplyModifiedProperties();
        }

        private void checkSuperClass(string superClassName)
        {
            var fileName = getFilePathByClassName(superClassName);
            if (File.Exists(fileName))
            {
                foreach (var line in File.ReadLines(fileName))
                {
                    if (line.IndexOf("---@class " + superClassName) != -1)
                    {
                        if (line != "---@class " + superClassName + ":Framework.UI.Prefab")
                        {
                            EditorUtility.DisplayDialog("警告!",
                                "在父类中没有找到继承自Framework.UI.Prefab的标记，请确认Prefab绑定类必须继承自Framework.UI.Prefab类型","确定");
                            return;
                        }
                    }
                }

            }
            else
            {
                if (EditorUtility.DisplayDialog("警告!",
                    string.Format("没有找到类{0}，请确认是否创建？", superClassName), "确定"))
                {
                    File.WriteAllText(fileName, string.Format(SUPER_CLASS_TEMPLATE, superClassName, Utils.MakeClassName(superClassName)));
                }
            }
        }
        private void saveAndCreate()
        {
            var go = (target as DocumentClass).gameObject;
            var trans = go.transform;
            var classFullName = m_LuaClass.stringValue.Replace("/", ".");
            if (classFullName == "")
            {
                EditorUtility.DisplayDialog("错误！", "LuaClass不能为空", "确定");
                return;
            }
            var className = Utils.MakeClassName(classFullName);
            var fileName = getFilePathByClassName(classFullName);
            var superClassName = m_SuperClass.stringValue;
            if (superClassName == "")
            {
                superClassName = "Framework.UI.Prefab";
            }
            else
            {
                checkSuperClass(superClassName);
            }
            
            var logicLines = new List<string>();
            if (File.Exists(fileName))
            {
                var state = 1;
                foreach (var line in File.ReadLines(fileName))
                {
                    switch (state)
                    {
                        case 1:
                            if (line == UI_END_CODE)
                            {
                                state = 2;
                            }
                            break;
                        case 2:
                            if (line.IndexOf("return") != 0)
                            {
                                logicLines.Add(line);
                            }
                            break;
                    }
                }
            }

            var head = UI_BEGIN_CODE;
//            类描述，包含类定义，继承，字段声明
            var classDesc = new List<string>();
            classDesc.Add(head);
            classDesc.Add(string.Format("---@type {0}", superClassName));
            classDesc.Add(string.Format("local super = require(\"{0}\")\n", superClassName));
            classDesc.Add(string.Format("---@class {0}:{1}", classFullName, superClassName));
            classDesc.Add(string.Format("{0} = class(\"{1}\", super)", className, classFullName));
//            printClassLines(classDesc);
            
//TODO 暂时不需要这个函数了，因为不打算在ui里面写逻辑
//            加载完成的回调函数，这里会把控件赋值给字段
//            var onCompletedFunction = new List<string>();
//            onCompletedFunction.Add("---OnComplete");
//            onCompletedFunction.Add("---@param evt Framework.event.Event");
//            onCompletedFunction.Add(string.Format("function {0}:OnComplete(evt)", className));
//            onCompletedFunction.Add("end");
            
//            BTLog.Error("classDesc:{0}", classDesc);
            _fields = new List<string[]>();
            createLuaFieldByTrans(trans, classDesc);

//            获取资源路径，会根据是不是editor进行区分
            var assetPath = AssetDatabase.GetAssetPath(go);
            var prefabStage = PrefabStageUtility.GetPrefabStage(go);
            if (prefabStage != null)
            {
                assetPath = prefabStage.prefabAssetPath;
                if (go != prefabStage.prefabContentsRoot)
                {
//                    TODO 目前编辑嵌套prefab时保存，不能正确取到prefab的assetpath
                    throw new Exception("不能在编辑别的prefab时，生成该prefab的代码");
                }
            }

            var fieldsStr = "";
            foreach (var field in _fields)
            {
                fieldsStr = fieldsStr + string.Format("\n\tself.{0} = {1}.New({2})\n\tself:AddChild(self.{0})", field[0], field[1], field[2]);
            }
            var CtorFunction = new List<string>();
            CtorFunction.Add(string.Format(
@"function {0}:ctor(autoBind)
    super.ctor(self){1}
    if autoBind ~= false then
        self:bindExtend()
    end
end
", className, fieldsStr));
            var GetAssetPathFunction = new List<string>();
            GetAssetPathFunction.Add(string.Format(
@"function {0}:GetAssetPath()
    return ""{1}""
end", className, assetPath));
            
//            调试代码
//            printClassLines(GetAssetPathFunction);

            classDesc.Add("");
            classDesc = classDesc.Concat(CtorFunction).ToList();
//            onCompletedFunction.Add("");
            classDesc = classDesc.Concat(GetAssetPathFunction).ToList();
            classDesc.Add("");
            classDesc.Add(UI_END_CODE);
            classDesc = classDesc.Concat(logicLines).ToList();
            classDesc.Add(string.Format("return {0}", className));
            File.WriteAllLines(fileName, classDesc);
        }

        private void createLuaFieldByTrans(Transform trans, List<string> classDesc)
        {
            BTLog.Debug("createLuaFieldByTrans:{0}", trans.name);
            var numChildren = trans.childCount;
            var nameList = new List<string>();
            for (var i = 0; i < numChildren; i++)
            {
                var child = trans.GetChild(i);
                var binder = child.GetComponent<GameObjectLuaBinder>();
                if (binder == null)
                {
                    createLuaFieldByTrans(child, classDesc);
                    continue;
                }
                var childName = child.name.Replace(" ", "_");
                if (nameList.Contains(childName))
                {
                    EditorUtility.DisplayDialog("错误!", string.Format("组件命名重复:{0}", childName), "确定");
                    return;
                }
                nameList.Add(childName);

                var typeName = binder.GetLuaClassName();
                classDesc.Insert(classDesc.Count - 1, string.Format("---@field {0} {1}", childName, typeName));
                _fields.Add(new []{childName, typeName.Substring(typeName.LastIndexOf(".") + 1), binder is DocumentClass?"false":""});
//                _Doc不用对其子go生成field
                if (binder is DocumentClass)
                {
//                    _Doc需要require一下
                    classDesc.Insert(1, string.Format("require(\"{0}\")", typeName));
                }
                else
                {
                    createLuaFieldByTrans(child, classDesc);
                }
            }
        }

        private string getFilePathByClassName(string className)
        {
            className = className.Replace(".", "/");
            className += ".lua";
            return LuaConst.luaDir + "/" + className;
        }

        //            调试代码
        private void printClassLines(List<string> lines)
        {
            foreach(var line in lines)
            {
                BTLog.Error("line:{0}", line);
            }
        }
    }
}