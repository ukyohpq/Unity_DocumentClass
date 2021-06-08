using System;
using LuaInterface;

namespace Framework.LuaUI
{
    public class LuaDisplayObjectContainer
    {
        public static void Extend(LuaState ls)
        {
            ls.LuaGetGlobal("DisplayObjectContainer");
            if (ls.LuaIsNil(-1))
            {
                ls.LuaPop(1);
                throw new Exception("can not find lua Class: DisplayObjectContainer");
            }
            ls.LuaPushFunction(AddChild);
            ls.LuaSetField(-2, "bind");
            ls.LuaPop(1);
        }

        private static int AddChild(IntPtr L)
        {
            return 0;
        }
    }
}