using System;
using Framework.core;
using LuaInterface;

namespace Framework.LuaUI.UIExtends
{
    public class LuaImage
    {
        public static void Extend(LuaState ls)
        {
            ls.LuaGetGlobal("Image");
            if (ls.LuaIsNil(-1))
            {
                ls.LuaPop(1);
                throw new Exception("can not find lua Class: Image");
            }
            ls.LuaPushFunction(SetImageExtend);
            ls.LuaSetField(-2, "SetImageExtend");
            ls.LuaPop(1);
        }

        private static int SetImageExtend(IntPtr L)
        {
            try
            {
                LuaTable tb;
                switch (LuaDLL.lua_gettop(L))
                {
                    case 2:
//                        通过路径加载
                        var path = LuaDLL.lua_tostring(L, -1);
                        tb = ToLua.ToVarObject(L, -2) as LuaTable;
                        LoaderManager.LoadImage(tb, path);
                        break;
                    case 3:
//                        通过图集加载
                        tb = ToLua.ToVarObject(L, -3) as LuaTable;
                        var atlas = LuaDLL.lua_tostring(L, -2);
                        var imgName = LuaDLL.lua_tostring(L, -1);
                        LoaderManager.LoadAtlasImage(tb, atlas, imgName);
                        break;
                    default:
                        throw new LuaException("number of args error");
                        break;
                }

                return 0;
            }
            catch (Exception e)
            {
                return LuaDLL.toluaL_exception(L, e);
            }
        }
    }
    
    
}