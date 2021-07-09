using FLuaUI.Components;
using UnityEngine.UI;

namespace FLuaUI.LuaUI.Components
{
    public class FLuaScrollViewPanel:GameObjectLuaBinder
    {
        public Image img;
        public override string GetLuaClassName()
        {
            return "Framework.UI.ScrollViewPanel";
        }
    }
}