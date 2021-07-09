using System;
using LuaInterface;
using UnityEngine;
using UnityEngine.UI;

namespace FLuaUI.LuaUI.UIExtends
{
    public class LuaInputField
    {
        public static void Extend(LuaState ls)
        {
            ls.LuaGetGlobal("InputField");
            if (ls.LuaIsNil(-1))
            {
                ls.LuaPop(1);
                throw new Exception("can not find lua Class: InputField");
            }
            ls.LuaPushFunction(SetInteractableExtend);
            ls.LuaSetField(-2, "SetInteractableExtend");
            ls.LuaPop(1);
        }

        private static int SetInteractableExtend(IntPtr L)
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
                var mono = ToLua.ToVarObject(L, -1) as MonoBehaviour;
                LuaDLL.lua_pop(L, 1);
                var input = mono.gameObject.GetComponent<InputField>();
                var b = LuaDLL.lua_toboolean(L, -1);
                input.interactable = b;
                return 0;
            }
            catch (Exception e)
            {
                return LuaDLL.toluaL_exception(L, e);
            }
        }
    }
}