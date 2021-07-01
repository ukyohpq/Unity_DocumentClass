using LuaInterface;

namespace Framework.LuaUI.UIExtends
{
    public static class LuaUIExtendManager
    {
        public static void InitExtends(LuaState ls)
        {
            LuaPrefab.Extend(ls);
            LuaImage.Extend(ls);
            LuaDisplayObjectContainer.Extend(ls);
            LuaTextField.Extend(ls);
            LuaDisplayObject.Extend(ls);
            LuaToggle.Extend(ls);
        }
    }
}