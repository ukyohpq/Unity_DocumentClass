using System;
using Babeltime.Log;
using Framework.UI;

namespace Framework.core
{
    public class Utils
    {
        public static Type GetTypeByComponentSuffix(string suffix)
        {
            Type T;
            switch (suffix)
            {
                case "Text":
                    return typeof(UnityEngine.UI.Text);
                case "Button":
                    return typeof(Framework.UI.Button);
                case "Doc":
                    return typeof(DocumentClass);
                default:
                    BTLog.Warning("未定义的后缀名");
                    return null;
            }
        }
    }
}