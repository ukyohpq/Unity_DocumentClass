using FLuaUI.Components;

namespace FLuaUI.LuaUI.Components
{
    public class FLuaNode:GameObjectLuaBinder
    {
        public override string GetLuaClassName()
        {
            return "Framework.display.Node";
        }
    }
}