using System;
using Babeltime.Log;
using Framework.core;
using Framework.core.loader;
using LuaInterface;

namespace Framework.LuaUI
{
    public static class LuaUIManager
    {
        public static void InitLuaUI(LuaState ls)
        {
            DocumentClass.Extend(ls);
        }
    }
}