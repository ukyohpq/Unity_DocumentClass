using System;
using Babeltime.Log;
using Framework.core;
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
            ls.LuaPushFunction(AddChildExtend);
            ls.LuaSetField(-2, "AddChildExtend");
            ls.LuaPop(1);
        }

        private static int AddChildExtend(IntPtr L)
        {
            try
            {
                BTLog.Error("before AddChildExtend");
                ToLua.CheckArgsCount(L, 2);
                LuaDLL.lua_pushvalue(L, -2);
                LuaDLL.lua_pushvalue(L, -2);
                LuaDLL.lua_gettable(L, LuaIndexes.LUA_REGISTRYINDEX);
                if (LuaDLL.lua_isnil(L, -1))
                {
                    LuaDLL.lua_pop(L, 2);
//                    此时child的gameobject还没有创建出来，就不用设置parent了
                    return 0;
                }
                var childBinder = ToLua.ToVarObject(L, -1) as GameObjectLuaBinder;
                BTLog.Error("childBinder:{0}", childBinder == null);
                if (childBinder != null)
                {
                    BTLog.Error("child:{0}", childBinder.name);
                }
                LuaDLL.lua_pop(L, 1);
                LuaDLL.lua_gettable(L, LuaIndexes.LUA_REGISTRYINDEX);
                var parentBinder = ToLua.ToVarObject(L, -1) as GameObjectLuaBinder;
                BTLog.Error("parent:{0}", parentBinder == null);
                if (parentBinder != null)
                {
                    BTLog.Error("parentBinder:{0}", parentBinder.name);
                }
//                BTLog.Error("parent:{0}, child:{0}", parentBinder.name, childBinder.name);
                childBinder.transform.parent = parentBinder.transform;
                BTLog.Error("after AddChildExtend");
            }
            catch (Exception e)
            { 
                return LuaDLL.toluaL_exception(L, e);
            }
            return 0;
        }
    }
}