using System;
using LuaInterface;

namespace Framework.core
{
    public class AssetsManager
    {
        public void RegisterLuaInterface(LuaState ls)
        {
            ls.LuaPushString("LoadImage");
            ls.LuaPushFunction(LoadImage);
        }

        private int LoadImage(IntPtr L)
        {
            return 1;
        }
    }
}