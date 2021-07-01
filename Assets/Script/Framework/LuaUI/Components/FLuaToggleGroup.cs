using System;
using Framework.core.Components;
using UnityEngine.UI;

namespace Script.Framework.LuaUI.Components
{
    public class FLuaToggleGroup:GameObjectLuaBinder
    {
        public override string GetLuaClassName()
        {
            return "Framework.UI.ToggleGroup";
        }
    }
}