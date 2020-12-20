using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Babeltime.Log;
using UnityEditor;
using Framework;
using Framework.UI;
using UnityEngine;
using System.Text;

namespace Framework.Editor
{
    [CustomEditor(typeof(DocumentClass))]
    public class DocumentClassEditor:UnityEditor.Editor
    {
        private SerializedProperty m_LuaClass;
        private SerializedProperty m_SuperClass;

        
        private void OnEnable()
        {
            m_LuaClass = serializedObject.FindProperty("LuaClass");
            m_SuperClass = serializedObject.FindProperty("SuperClass");
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
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(m_SuperClass, new GUIContent("SuperClass"));
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

        private void saveAndCreate()
        {
            BTLog.Debug("saveAndCreate GameObject:{0}", (this.target as DocumentClass).gameObject);
            var trans = (this.target as DocumentClass).gameObject.transform;
            var children = trans.GetComponentsInChildren<Transform>();
            var filePath = FrameConst.CUSTOM_UI_DIR;
            var fileName = filePath + trans.name + ".lua";
//            if (File.Exists(filePath + trans.name + ".lua"))
//            {
//                File.Delete(fileName);
//            }
//            File.Create(fileName);
            var classDesc = new List<string>();
            var lines = new List<string>();
            var superClassName = m_SuperClass.stringValue;
            var className = fileName.Replace("/", ".");
            BTLog.Error("className:{0}", className);
//            deleta "xxxxx.Assets.Lua."
            var headMark = "Assets.Lua.";
            className = className.Substring(className.IndexOf(headMark) + headMark.Length);
            BTLog.Error("className:{0}", className);
//            delete ".lua"
            var tailMark = ".lua";
            className = className.Remove(className.IndexOf(tailMark));
            BTLog.Error("className:{0}", className);
            if (superClassName == "")
            {
                classDesc.Add(string.Format("---@class {0}", className));
            }
            else
            {
                classDesc.Add(string.Format("---@class {0}:{1}", className, superClassName));
            }
            foreach (var child in children)
            {
                var childName = child.name;
                if (childName.Substring(0, 2) == "m_")
                {
                    var suffixIndex = childName.LastIndexOf("_");
                    var suffix = childName.Substring(suffixIndex + 1);
                    var luaCode = String.Format("self.{0} = aa", childName);
                    classDesc.Add(string.Format("---@field {0}",childName));
                    lines.Add(luaCode);
                    BTLog.Error("name:{0} suffix:{1} count:{2}", childName, suffix, lines.Count);
                }
            }
            BTLog.Error("lines:{0}", lines.Count);
            File.WriteAllLines(fileName, classDesc.Concat(lines));
        }
    }
}