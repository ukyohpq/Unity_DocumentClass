using LuaInterface;

namespace Framework.LuaUI.UIExtends
{
    public static class LuaUIManager
    {
        public static void InitLuaUI(LuaState ls)
        {
            LuaPrefab.Extend(ls);
            LuaImage.Extend(ls);
            LuaDisplayObjectContainer.Extend(ls);
            LuaTextField.Extend(ls);
        }
    }
}