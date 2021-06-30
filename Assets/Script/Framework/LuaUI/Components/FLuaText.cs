using Framework.core.Components;

namespace Script.Framework.LuaUI.Components
{
    public class FLuaText:GameObjectLuaBinder
    {
        public override string GetLuaClassName()
        {
            return "Framework.UI.TextField";
        }
    }
}