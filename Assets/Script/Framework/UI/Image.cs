using System;
using Framework.core;
using LuaInterface;

namespace Framework.UI
{
    public class Image:GameObjectLuaBinder
    {
        public override void CreatePrefabAndBindLuaClass(LuaState luaState)
        {
            base.CreatePrefabAndBindLuaClass(luaState);
            PushLuaInstance(luaState, "Image");
            luaState.LuaPushFunction(SetImage);
        }

        private static int SetImage(IntPtr L)
        {
            try
            {
                switch (LuaDLL.lua_gettop(L))
                {
                    case 2:
//                        通过路径加载
                        var path = LuaDLL.lua_tostring(L, -1);
                        break;
                    case 3:
//                        通过图集加载
                        var altas = LuaDLL.lua_tostring(L, -2);
                        var imgName = LuaDLL.lua_tostring(L, -1);
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