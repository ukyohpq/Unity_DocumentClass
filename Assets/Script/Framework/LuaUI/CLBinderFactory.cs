using System;
using Babeltime.Log;
using Framework.core;
using Framework.core.loader;
using LuaInterface;

namespace Framework.LuaUI
{
    public static class CLBinderFactory
    {
        public static void MakeCLBinder(LuaState ls)
        {
            ls.LuaGetGlobal("Prefab");
            if (ls.LuaIsNil(-1))
            {
                ls.LuaPop(1);
                throw new Exception("can not find lua Class: Prefab");
            }
            ls.LuaPushFunction(PrefabBind);
            ls.LuaSetField(-2, "bind");
            ls.LuaPop(1);
        }

        private static int PrefabBind(IntPtr L)
        {
            try
            {
                ToLua.CheckArgsCount(L, 1);
//                dup self
                LuaDLL.lua_pushvalue(L, -1);
                LuaDLL.lua_getglobal(L, "Prefab");
                LuaDLL.lua_getfield(L, -1, "super");
//                Prefab.super:bind(self)
                LuaDLL.lua_getfield(L, -1, "bind");
//                self, self, Prefab, super, bind
                LuaDLL.lua_insert(L, -4);
//                self, bind, self, Prefab, super
                LuaDLL.lua_pop(L, 2);
//                self, bind, self,
                LuaDLL.lua_call(L, 1, 0);
                
                var tb = ToLua.ToLuaTable(L, -1);
                CSBridge.LoadPrefab(tb);
                
                LuaDLL.lua_getfield(L, -1, "afterBind");
                LuaDLL.lua_setfield(L, -2, "bind");
                
                LuaDLL.lua_pushcfunction(L, DestroyFromLua);
                LuaDLL.lua_setfield(L, -2, "DestroyToCS");
                return 0;
            }
            catch (Exception e)
            {
                return LuaDLL.toluaL_exception(L, e);
            }
        }

        private static int DestroyFromLua(IntPtr L)
        {
            try
            {
                ToLua.CheckArgsCount(L, 1);
                LuaDLL.lua_pushnil(L);
                LuaDLL.lua_setfield(L, -2, "DestroyToCS");
                var tb = ToLua.ToLuaTable(L, -1);
                CSBridge.StopLoad(tb);
                return 0;
            }
            catch (Exception e)
            {
                return LuaDLL.toluaL_exception(L, e);
            }
        }
    }
}