using System;
using Framework.core;
using LuaInterface;

namespace Framework.LuaUI
{
    public class LuaImage:GameObjectLuaBinder
    {
        public override void CreatePrefabAndBindLuaClass(LuaState luaState)
        {
            base.CreatePrefabAndBindLuaClass(luaState);
            PushLuaInstance(luaState, "Image");
            luaState.LuaPushFunction(SetImage);
            luaState.LuaSetField(-2, "SetImage");
        }

        private static int SetImage(IntPtr L)
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
                        CSBridge.LoadImage(tb, path);
                        break;
                    case 3:
//                        通过图集加载
                        tb = ToLua.ToVarObject(L, -3) as LuaTable;
                        var atlas = LuaDLL.lua_tostring(L, -2);
                        var imgName = LuaDLL.lua_tostring(L, -1);
                        CSBridge.LoadAtlasImage(tb, atlas, imgName);
                        break;
                    default:
                        throw new LuaException("number of args error");
                        break;
                }

                return 0;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
    
    
}