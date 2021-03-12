﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Babeltime.Log;
using UnityEditor;
using Framework;
using Framework.UI;
using UnityEngine;
using System.Text;
using Framework.core;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine.UI;
using Button = Framework.UI.Button;

namespace Framework.Editor
{
    [CustomEditor(typeof(DocumentClass))]
    public class DocumentClassEditor:UnityEditor.Editor
    {
        private SerializedProperty m_LuaClass;
//        private SerializedProperty m_SuperClass;

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
            GUILayout.Label("ReadMe");
            EditorGUILayout.EndHorizontal();
//            ReadMe
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("ReadMe content");
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

        private Type getTypeBySuffix(string suffix)
        {
            switch (suffix)
            {
                case "Text":
                    return typeof(Text);
                case "Button":
                    return typeof(Button);
                default:
                    throw new Exception(string.Format("invalid component suffix:{}", suffix));
            }
        }
        private void saveAndCreate()
        {
            var go = (this.target as DocumentClass).gameObject;
//            var goclone = GameObject.Instantiate(go);
//            goclone.transform.parent = GameObject.Find("UIRoot").transform;
            BTLog.Debug("saveAndCreate GameObject:{0} {1}", AssetDatabase.GetAssetPath(go), AssetDatabase.GetAssetOrScenePath(go));
//            GameObject.DestroyImmediate(goclone);
            var trans = go.transform;
            var filePath = FrameConst.CUSTOM_UI_DIR;
            var className = trans.name;
            var fileName = filePath + className + ".lua";
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            var classFullName = makeClassName(fileName);
//            类描述，包含类定义，继承，字段声明
            var classDesc = new List<string>();
//            var superClassName = m_SuperClass.stringValue;
            var superClassName = "Framework.UI.Prefab";

            classDesc.Add(string.Format("---@type {0}", superClassName));
            classDesc.Add(string.Format("local super = require(\"{0}\")\n", superClassName));
            classDesc.Add(string.Format("---@class {0}:{1}", classFullName, superClassName));
            classDesc.Add(string.Format("{0} = class(\"{1}\", super)", className, classFullName));
            printClassLines(classDesc);
            
//            加载完成的回调函数，这里会把控件赋值给字段
            var onCompletedFunction = new List<string>();
            onCompletedFunction.Add(string.Format("function {0}:OnComplete(ui)", className));
            onCompletedFunction.Add("end");
            
//            BTLog.Error("classDesc:{0}", classDesc);
            var children = trans.GetComponentsInChildren<Transform>();
            foreach (var child in children)
            {
                var childName = child.name;
                if (childName.Substring(0, 2) == "m_")
                {
                    var suffixIndex = childName.LastIndexOf("_");
                    var suffix = childName.Substring(suffixIndex + 1);
                    classDesc.Insert(classDesc.Count - 1, string.Format("---@field {0} {1}", childName, Utils.GetTypeByComponentSuffix(suffix)));
                    BTLog.Error("name:{0} suffix:{1} count:{2}", childName, suffix, onCompletedFunction.Count);
                }
            }

//            获取资源路径，会根据是不是editor进行区分
            var assetPath = AssetDatabase.GetAssetPath(go);
            var prefabStage = PrefabStageUtility.GetPrefabStage(go);
            if (prefabStage != null)
            {
                assetPath = prefabStage.prefabAssetPath;
            }
            BTLog.Error("assetPath:{0}", assetPath);
            var GetAssetPathFunction = new List<string>();
            GetAssetPathFunction.Add(string.Format(@"function {0}:GetAssetPath()
    if IsEditor then
        return ""{1}""
    else
        return ""{2}""
    end
end", className, assetPath,2));
            
//            调试代码
//            printClassLines(GetAssetPathFunction);

            BTLog.Error("lines:{0}", onCompletedFunction.Count);
            
            classDesc.Add("");
            onCompletedFunction.Add("");
            var classLines = classDesc.Concat(onCompletedFunction).Concat(GetAssetPathFunction);
            classLines = classLines.Append(string.Format(@"
return {0}", className));
            File.WriteAllLines(fileName, classLines);
        }

        private string makeClassName(string fileName)
        {
//            可以在里面写"/"或者"."，最后都会统一把"/"替换成"."
            var className = fileName.Replace("/", ".");
//            BTLog.Error("className:{0}", className);
//            删除头部的 "xxxxx.Assets.Lua."，截取后面的字符串
            var headMark = "Assets.Lua.";
            className = className.Substring(className.IndexOf(headMark) + headMark.Length);
            BTLog.Error("className:{0}", className);
//            delete ".lua"
            var tailMark = ".lua";
            className = className.Remove(className.IndexOf(tailMark));
//            BTLog.Error("className:{0}", className);
            return className;
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