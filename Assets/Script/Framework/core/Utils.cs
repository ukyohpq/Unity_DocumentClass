using System;
using Babeltime.Log;
using Framework.UI;
using UnityEngine;

namespace Framework.core
{
    public class Utils
    {
        public static Type GetTypeByComponentSuffix(string suffix)
        {
            Type T;
            switch (suffix)
            {
                case "_Text":
                    return typeof(UnityEngine.UI.Text);
                case "_Button":
                    return typeof(Framework.UI.Button);
                default:
                    BTLog.Warning("未定义的后缀名");
                    return null;
            }
        }
        
        public static string GetTypeNameByComponentSuffix(string suffix, Transform trans)
        {
            Type T;
            switch (suffix)
            {
                case "_Text":
                    return "UnityEngine.UI.Text";
                case "_Button":
                    return "Framework.UI.Button";
                case "_Doc":
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
        
        public static bool IsValidSuffix(string suffix)
        {
            switch (suffix)
            {
                case "_Text":
                case "_Button":
                case "_Doc":
                    return true;
                default:
                    return false;
            }
        }
    }
}