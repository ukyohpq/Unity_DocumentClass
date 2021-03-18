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
            var classFullName = m_LuaClass.stringValue.Replace("/", ".");
            var className = Utils.MakeClassName(classFullName);
            var fileName = getFilePathByClassName(classFullName);
            if (File.Exists(fileName))
            {
//                TODO 需要弹出对话框提示用户目标文件已经存在，是否覆盖。目前静默覆盖
                File.Delete(fileName);
            }
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
            onCompletedFunction.Add("---OnComplete");
            onCompletedFunction.Add("---@param evt Framework.event.Event");
            onCompletedFunction.Add(string.Format("function {0}:OnComplete(evt)", className));
            onCompletedFunction.Add("end");
            
//            BTLog.Error("classDesc:{0}", classDesc);
            createLuaFieldByTrans(trans, classDesc);

//            获取资源路径，会根据是不是editor进行区分
            var assetPath = AssetDatabase.GetAssetPath(go);
            var prefabStage = PrefabStageUtility.GetPrefabStage(go);
            if (prefabStage != null)
            {
                assetPath = prefabStage.prefabAssetPath;
            }
            BTLog.Error("assetPath:{0}", assetPath);
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

            BTLog.Error("lines:{0}", onCompletedFunction.Count);
            
            classDesc.Add("");
            onCompletedFunction.Add("");
            var classLines = classDesc.Concat(onCompletedFunction).Concat(GetAssetPathFunction);
            classLines = classLines.Append(string.Format(@"
return {0}", className));
            File.WriteAllLines(fileName, classLines);
        }

        private void createLuaFieldByTrans(Transform trans, List<string> classDesc)
        {
            BTLog.Error("createLuaFieldByTrans:{0}", trans.name);
            var numChildren = trans.childCount;
            var nameList = new List<string>();
            for (var i = 0; i < numChildren; i++)
            {
                var child = trans.GetChild(i);
                var childName = child.name;
                if (nameList.Contains(childName))
                {
                    throw new Exception(string.Format("组件命名重复:{0}", childName));
                }
                nameList.Add(childName);
                var suffixIndex = childName.LastIndexOf("_");
                var suffix = "";
                if (suffixIndex != -1)
                {
                    suffix = childName.Substring(suffixIndex);
                }
                if (Utils.IsValidSuffix(suffix))
                {
                    classDesc.Insert(classDesc.Count - 1, string.Format("---@field {0} {1}", childName, Utils.GetTypeNameByComponentSuffix(suffix, child)));
                    if (suffix != "_Doc")
                    {
                        createLuaFieldByTrans(child, classDesc);
                    }
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