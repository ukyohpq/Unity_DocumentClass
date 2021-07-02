using System;
using LuaInterface;
using UnityEngine;
using UnityEngine.UI;

namespace Framework.LuaUI.UIExtends
{
    public class LuaToggle
    {
        public static void Extend(LuaState ls)
        {
            ls.LuaGetGlobal("Toggle");
            if (ls.LuaIsNil(-1))
            {
                ls.LuaPop(1);
                throw new Exception("can not find lua Class: Toggle");
            }
            ls.LuaPushFunction(SetIsOnExtend);
            ls.LuaSetField(-2, "SetIsOnExtend");
            ls.LuaPop(1);
        }

        private static int SetIsOnExtend(IntPtr L)
        {
            try
            {
                ToLua.CheckArgsCount(L, 2);
                LuaDLL.lua_pushvalue(L, -2);
                LuaDLL.lua_gettable(L, LuaIndexes.LUA_REGISTRYINDEX);
                if (LuaDLL.lua_isnil(L, -1))
                {
                    LuaDLL.lua_pop(L, 1);
                    return 0;
                }
                var binder = ToLua.ToVarObject(L, -1) as MonoBehaviour;
                LuaDLL.lua_pop(L, 1);
                var toggle = binder.gameObject.GetComponent<Toggle>();
                if (toggle == null)
                {
                    throw new Exception(string.Format("can not find Component Toggle:{0}", binder.name));
                }
                var isOn = LuaDLL.tolua_toboolean(L, -1);
                toggle.isOn = isOn;
            }
            catch (Exception e)
            {
                return LuaDLL.toluaL_exception(L, e);
            }
            return 0;
        }
    }
}