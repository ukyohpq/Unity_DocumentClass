using System;
using System.Collections.Generic;
using Babeltime.Log;
using Framework.core;
using LuaInterface;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Framework.LuaUI
{
    public class LuaButton:GameObjectLuaBinder, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
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
            ls.LuaGetField(-1, "DispatchMessage");
            ls.LuaInsert(-2);
            ls.LuaPushString(eventName);
            ls.LuaSafeCall(2, 0, 0, 0);
        }
        
        public override void CreatePrefabAndBindLuaClass(LuaState luaState)
        {
            base.CreatePrefabAndBindLuaClass(luaState);
            PushLuaInstance(luaState, "Button");
        }
        
    }
}