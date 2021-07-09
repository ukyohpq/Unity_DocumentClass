using FLuaUI.Components;

namespace FLuaUI.LuaUI.Components
{
    public class FLuaImage:GameObjectLuaBinder
    {
        public bool AutoNativeSize;
        public override string GetLuaClassName()
        {
            return "Framework.UI.Image";
        }
    }
}