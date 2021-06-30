using Framework.core.Components;
using Framework.LuaUI.Components;

namespace Script.Framework.LuaUI.Components
{
    public class FLuaImage:GameObjectLuaBinder
    {
        public override string GetLuaClassName()
        {
            return "Framework.UI.Image";
        }
    }
}