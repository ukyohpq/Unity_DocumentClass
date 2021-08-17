using System;
using FLuaUI.core;
using LuaInterface;
using UnityEngine;
using UnityEngine.UI;

namespace FLuaUI.LuaUI.UIExtends
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
            ls.LuaPushFunction(SetFillAmountExtend);
            ls.LuaSetField(-2, "SetFillAmountExtend");
            ls.LuaPushFunction(GetSizeExtend);
            ls.LuaSetField(-2, "GetSizeExtend");
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
                        LoaderManager.LoadImage(tb,  path);
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

        private static int SetFillAmountExtend(IntPtr L)
        {
            try
            {
                ToLua.CheckArgsCount(L, 2);
                LuaDLL.lua_pushvalue(L, -2);
                LuaDLL.lua_gettable(L, LuaIndexes.LUA_REGISTRYINDEX);
                var binder = ToLua.ToVarObject(L, -1) as MonoBehaviour;
                var img = binder.GetComponent<Image>();
                var fillAmout = LuaDLL.lua_tonumber(L, -2);
                img.fillAmount = (float)fillAmout;
                LuaDLL.lua_pop(L, 1);
                return 0;
            }
            catch (Exception e)
            {
                return LuaDLL.toluaL_exception(L, e);
            }
        }

        private static int GetSizeExtend(IntPtr L)
        {
            try
            {
                ToLua.CheckArgsCount(L, 1);
                LuaDLL.lua_pushvalue(L, -1);
                LuaDLL.lua_gettable(L, LuaIndexes.LUA_REGISTRYINDEX);
                var binder = ToLua.ToVarObject(L, -1) as MonoBehaviour;
                LuaDLL.lua_pop(L, 1);
                var rect = binder.GetComponent<Image>().sprite.rect;
                LuaDLL.lua_pushnumber(L, rect.width);
                LuaDLL.lua_pushnumber(L, rect.height);
                return 2;
            }
            catch (Exception e)
            {
                return LuaDLL.toluaL_exception(L, e);
            }
        }
    }
    
    
}