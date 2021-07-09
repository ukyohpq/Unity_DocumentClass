using System;
using FLuaUI.Components;
using LuaInterface;
using UnityEngine;

namespace FLuaUI.LuaUI.UIExtends
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
            ls.LuaPushFunction(AddChildExtend);
            ls.LuaSetField(-2, "AddChildExtend");
            ls.LuaPop(1);
        }

        private static int AddChildExtend(IntPtr L)
        {
            try
            {
                ToLua.CheckArgsCount(L, 2);
                LuaDLL.lua_pushvalue(L, -2);
                LuaDLL.lua_pushvalue(L, -2);
                LuaDLL.lua_gettable(L, LuaIndexes.LUA_REGISTRYINDEX);
                if (LuaDLL.lua_isnil(L, -1))
                {
                    LuaDLL.lua_pop(L, 2);
                    return 0;
                }
                var childBinder = ToLua.ToVarObject(L, -1) as MonoBehaviour;
                LuaDLL.lua_pop(L, 1);
                LuaDLL.lua_gettable(L, LuaIndexes.LUA_REGISTRYINDEX);
                var parentBinder = ToLua.ToVarObject(L, -1) as GameObjectLuaBinder;
                childBinder.transform.parent = parentBinder.Container;
            }
            catch (Exception e)
            { 
                return LuaDLL.toluaL_exception(L, e);
            }
            return 0;
        }
    }
}