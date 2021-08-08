using System;
using FLuaUI.Components;
using FLuaUI.LuaUI.Components;
using LuaInterface;
using UnityEngine;

namespace FLuaUI.LuaUI.UIExtends
{
    public class LuaDisplayObject
    {
        public static void Extend(LuaState ls)
        {
            ls.LuaGetGlobal("DisplayObject");
            if (ls.LuaIsNil(-1))
            {
                ls.LuaPop(1);
                throw new Exception("can not find lua Class: DisplayObject");
            }
            ls.LuaPushFunction(SetActiveExtend);
            ls.LuaSetField(-2, "SetActiveExtend");
            ls.LuaPushFunction(StartDragExtend);
            ls.LuaSetField(-2, "StartDragExtend");
            ls.LuaPushFunction(StopDragExtend);
            ls.LuaSetField(-2, "StopDragExtend");
            ls.LuaPushFunction(BackToStartPointExtend);
            ls.LuaSetField(-2, "BackToStartPointExtend");
            ls.LuaPop(1);
        }

        private static int SetActiveExtend(IntPtr L)
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
                var binder = ToLua.ToVarObject(L, -1) as GameObjectLuaBinder;
                var active = LuaDLL.lua_toboolean(L, -2);
                binder.gameObject.SetActive(active);
                LuaDLL.lua_pop(L, 2);
            }
            catch (Exception err)
            {
                return LuaDLL.toluaL_exception(L, err);
            }

            return 0;
        }

        private static int StartDragExtend(IntPtr L)
        {
            try
            {
                ToLua.CheckArgsCount(L, 1);
                LuaDLL.lua_pushvalue(L, -1);
                LuaDLL.lua_gettable(L, LuaIndexes.LUA_REGISTRYINDEX);
                if (LuaDLL.lua_isnil(L, -1))
                {
                    LuaDLL.lua_pop(L, 1);
                    return 0;
                }

                var binder = ToLua.ToVarObject(L, -1) as GameObjectLuaBinder;
                var dragHandler = binder.gameObject.GetComponent<DragHandler>();
                if (dragHandler == null)
                {
                    dragHandler = binder.gameObject.AddComponent<DragHandler>();
                }

                dragHandler.StartDrag();
                LuaDLL.lua_pop(L, 1);
            }
            catch (Exception err)
            {
                return LuaDLL.toluaL_exception(L, err);
            }

            return 0;
        }

        private static int StopDragExtend(IntPtr L)
        {
            try
            {
                ToLua.CheckArgsCount(L, 1);
                LuaDLL.lua_pushvalue(L, -1);
                LuaDLL.lua_gettable(L, LuaIndexes.LUA_REGISTRYINDEX);
                if (LuaDLL.lua_isnil(L, -1))
                {
                    LuaDLL.lua_pop(L, 1);
                    return 0;
                }

                var binder = ToLua.ToVarObject(L, -1) as GameObjectLuaBinder;
                var dragHandler = binder.gameObject.GetComponent<DragHandler>();
                if (dragHandler == null)
                {
                    LuaDLL.lua_pop(L, 1);
                    return 0;
                }

                dragHandler.StopDrag();
                LuaDLL.lua_pop(L, 1);
            }
            catch (Exception err)
            {
                return LuaDLL.toluaL_exception(L, err);
            }

            return 0; 
        }

        private static int BackToStartPointExtend(IntPtr L)
        {
            try
            {
                ToLua.CheckArgsCount(L, 1);
                LuaDLL.lua_pushvalue(L, -1);
                LuaDLL.lua_gettable(L, LuaIndexes.LUA_REGISTRYINDEX);
                if (LuaDLL.lua_isnil(L, -1))
                {
                    LuaDLL.lua_pop(L, 1);
                    return 0;
                }

                var binder = ToLua.ToVarObject(L, -1) as GameObjectLuaBinder;
                var dragHandler = binder.gameObject.GetComponent<DragHandler>();
                if (dragHandler == null)
                {
                    LuaDLL.lua_pop(L, 1);
                    return 0;
                }

                dragHandler.BackToDragStartPoint();
                LuaDLL.lua_pop(L, 1);
            }
            catch (Exception err)
            {
                return LuaDLL.toluaL_exception(L, err);
            }

            return 0;
        }
    }
}