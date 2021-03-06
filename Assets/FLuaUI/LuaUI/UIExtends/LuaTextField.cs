using System;
using LuaInterface;
using UnityEngine;
using UnityEngine.UI;

namespace FLuaUI.LuaUI.UIExtends
{
    public class LuaTextField
    {
        public static void Extend(LuaState ls)
        {
            ls.LuaGetGlobal("TextField");
            if (ls.LuaIsNil(-1))
            {
                ls.LuaPop(1);
                throw new Exception("can not find lua Class: TextField");
            }
            ls.LuaPushFunction(SetTextExtend);
            ls.LuaSetField(-2, "SetTextExtend");
            ls.LuaPop(1);
        }

        private static int SetTextExtend(IntPtr L)
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
                var text = LuaDLL.lua_tostring(L, -1);
                var textField = binder.gameObject.GetComponent<Text>();
                textField.text = text;
            }
            catch (Exception e)
            {
                return LuaDLL.toluaL_exception(L, e);
            }

            return 0;
        }
    }
}