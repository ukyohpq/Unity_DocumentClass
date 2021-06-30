using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Babeltime.Log;
using UnityEditor;
using UnityEngine;
using Framework.core;
using Framework.core.Components;
using Framework.LuaUI.Components;
using UnityEditor.Experimental.SceneManagement;

namespace Framework.Editor
{
    [CustomEditor(typeof(DocumentClass))]
    public class DocumentClassEditor:UnityEditor.Editor
    {
        private SerializedProperty m_LuaClass;
//        private SerializedProperty m_SuperClass;

        private List<string[]> _fields;
        private void OnEnable()
        {
            m_LuaClass = serializedObject.FindProperty("LuaClass");
//            m_SuperClass = serializedObject.FindProperty("SuperClass");
            if (m_LuaClass.stringValue == "")
            {
                m_LuaClass.stringValue = serializedObject.targetObject.name;
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
//            LuaClassName
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(m_LuaClass, new GUIContent("LuaClass"));
            EditorGUILayout.EndHorizontal();
//            SuperClassName
//            EditorGUILayout.BeginHorizontal();
//            EditorGUILayout.PropertyField(m_SuperClass, new GUIContent("SuperClass"));
//            EditorGUILayout.EndHorizontal();
            
//            saveAndCreate
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("save"))
            {
                saveAndCreate();
            }
            EditorGUILayout.EndHorizontal();
            
            serializedObject.ApplyModifiedProperties();
        }
        
        private void saveAndCreate()
        {
            var go = (target as DocumentClass).gameObject;
            var trans = go.transform;
            var classFullName = m_LuaClass.stringValue.Replace("/", ".");
            var className = Utils.MakeClassName(classFullName);
            var fileName = getFilePathByClassName(classFullName);
            if (File.Exists(fileName))
            {
                if(EditorUtility.DisplayDialog("警告!", "源文件已经存在，点击确定将进行覆盖", "覆盖", "放弃"))
                {
                    File.Delete(fileName);
                }
                else
                {
                    return;
                }
            }

            var head = @"------------------------------------------------------------------------------------------
----------
----------    不要修改ui代码！
----------    不要修改ui代码！！
----------    不要修改ui代码！！！
----------
------------------------------------------------------------------------------------------
";
//            类描述，包含类定义，继承，字段声明
            var classDesc = new List<string>();
            classDesc.Add(head);
//            var superClassName = m_SuperClass.stringValue;
            var superClassName = "Framework.UI.Prefab";

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
    super.ctor(self, autoBind){1}
end
", className, fieldsStr));
            var GetAssetPathFunction = new List<string>();
            GetAssetPathFunction.Add(string.Format(
@"function {0}:GetAssetPath()
    if IsEditor then
        return ""{1}""
    else
        return ""{2}""
    end
end", className, assetPath,2));
            
//            调试代码
//            printClassLines(GetAssetPathFunction);

            classDesc.Add("");
            classDesc = classDesc.Concat(CtorFunction).ToList();
//            onCompletedFunction.Add("");
            classDesc = classDesc.Concat(GetAssetPathFunction).ToList();
            classDesc.Add(string.Format(@"
return {0}", className));
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
                var childName = child.name;
                if (nameList.Contains(childName))
                {
                    throw new Exception(string.Format("组件命名重复:{0}", childName));
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