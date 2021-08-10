namespace FLuaUI.LuaUI.Components
{
    public class FLuaImage:PointerEventHandler
    {
        public bool AutoNativeSize;
        public override string GetLuaClassName()
        {
            return "Framework.UI.Image";
        }
    }
}