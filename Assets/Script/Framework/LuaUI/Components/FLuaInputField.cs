using System;
using Framework.core.Components;
using UnityEngine.UI;

namespace Script.Framework.LuaUI.Components
{
    public class FLuaInputField:GameObjectLuaBinder
    {
        private void Awake()
        {
            var input = gameObject.GetComponent<InputField>();
//            input.
        }

        public override string GetLuaClassName()
        {
            return "Framework.UI.InputField";
        }
    }
}