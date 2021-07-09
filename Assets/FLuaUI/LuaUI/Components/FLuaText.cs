using FLuaUI.Components;

namespace FLuaUI.LuaUI.Components
{
    public class FLuaText:GameObjectLuaBinder
    {
        public override string GetLuaClassName()
        {
            return "Framework.UI.TextField";
        }
    }
}