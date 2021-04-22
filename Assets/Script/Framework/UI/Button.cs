using System;
using System.Collections.Generic;
using Babeltime.Log;
using Framework.core;
using LuaInterface;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Framework.UI
{
    public class Button:GameObjectLuaBinder, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            DispatchEvent("click");
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            DispatchEvent("down");
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            DispatchEvent("up");
        }

        public void DispatchEvent(string eventName)
        {
            var ls = GetLuaState();
            PushLuaTable();
            ls.LuaDup();
            ls.LuaGetField(-1, "DispatchMessage");
            ls.LuaInsert(-2);
            ls.LuaPushString(eventName);
            ls.LuaSafeCall(2, 0, 0, 0);
        }
        
        public override void CreatePrefabAndBindLuaClass(LuaState luaState)
        {
            base.CreatePrefabAndBindLuaClass(luaState);
            var curTop = luaState.LuaGetTop();
            var className = "Button";
            luaState.LuaGetGlobal(className);
            if (luaState.LuaIsNil(-1))
            {
                luaState.LuaSetTop(curTop);
                BTLog.Error("can not find lua class:{0}", className);
                return;
            }
            luaState.LuaGetField(-1, "New");
            if (luaState.LuaIsNil(-1))
            {
                luaState.LuaSetTop(curTop);
                BTLog.Error("can not find constructor for lua class:{0}", className);
                return;
            }
            
            luaState.LuaSafeCall(0, 1, 0, curTop);
            var luaButton = luaState.ToVariant(-1) as LuaTable;
            //删除类，保留实例，并且栈上留下实例，下一步直接使用，不用删了再创建
            luaState.LuaRemove(-2);
            BindLuaTable(luaButton);
        }
        
    }
}