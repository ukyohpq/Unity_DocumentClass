using FLuaUI.LuaUI.Components;

namespace Script.Framework.LuaUI.Components
{
    public class FLuaButton:PointerEventHandler
    {
        public override string GetLuaClassName()
        {
            return "Framework.UI.Button";
        }
    }
}