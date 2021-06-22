using System;
using Babeltime.Log;
using LuaInterface;
using UnityEngine;

namespace Framework.LuaUI.UIExtends
{
    public class LuaScrollView
    {
        public static void Extend(LuaState ls)
        {
            ls.LuaGetGlobal("ScrollView");
            if (ls.LuaIsNil(-1))
            {
                ls.LuaPop(1);
                throw new Exception("can not find lua Class: ScrollView");
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
                var childBinder = ToLua.ToVarObject(L, -1) as MonoBehaviour;
                BTLog.Error("childBinder:{0}", childBinder == null);
                if (childBinder != null)
                {
                    BTLog.Error("child:{0}", childBinder.name);
                }
                LuaDLL.lua_pop(L, 1);
                LuaDLL.lua_gettable(L, LuaIndexes.LUA_REGISTRYINDEX);
                var SVBinder = ToLua.ToVarObject(L, -1) as MonoBehaviour;
                BTLog.Error("SVBinder:{0}", SVBinder == null);
                if (SVBinder != null)
                {
                    BTLog.Error("SVBinder:{0}", SVBinder.name);
                }
//                BTLog.Error("parent:{0}, child:{0}", parentBinder.name, childBinder.name);
                var content = SVBinder.transform.Find("Content");
                childBinder.transform.parent = content;
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