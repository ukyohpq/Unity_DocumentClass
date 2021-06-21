using System;
using Babeltime.Log;
using Framework.LuaUI;
using Framework.LuaUI.Components;
using Framework.LuaUI.UIExtends;
using UnityEngine;

namespace Framework.core
{
    public class ComponentSuffix
    {
        public const string Doc = "_Doc";
        public const string Button = "_Button";
        public const string Text = "_Text";
        public const string Image = "_Image";
        public const string ScrollView = "_SV";
    }
    
    public class Utils
    {
        public static string GetTypeNameByComponentSuffix(string suffix, Transform trans)
        {
            switch (suffix)
            {
                case ComponentSuffix.Text:
                    return "Framework.UI.TextField";
                case ComponentSuffix.Button:
                    return "Framework.UI.Button";
                case ComponentSuffix.Image:
                    return "Framework.UI.Image";
                case ComponentSuffix.ScrollView:
                    return "Framework.UI.ScrollView";
                case ComponentSuffix.Doc:
                    var doc = trans.GetComponent<DocumentClass>();
                    if (doc == null)
                    {
                        BTLog.Error("以Doc为后缀的名称的组件，必须拥有DocumentClass组件");
                        return "";
                    }
                    return doc.GetLuaClassName();
                default:
                    BTLog.Warning("未定义的后缀名");
                    return null;
            }
        }
        
        public static string MakeClassName(string fullName)
        {
            var lastPointIndex = fullName.LastIndexOf(".");
            return fullName.Substring(lastPointIndex + 1);
        }
        
        public static string GetSuffixOfGoName(string goName)
        {
            var index = goName.LastIndexOf("_");
            if (index == -1)
            {
                return "";
            }
            return goName.Substring(index);
        }
        
        public static bool IsValidSuffix(string suffix)
        {
            switch (suffix)
            {
                case ComponentSuffix.Text:
                case ComponentSuffix.Button:
                case ComponentSuffix.Image:
                case ComponentSuffix.ScrollView:
                case ComponentSuffix.Doc:
                    return true;
                default:
                    return false;
            }
        }
    }
}