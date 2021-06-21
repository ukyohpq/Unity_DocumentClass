using System;
using System.Collections.Generic;
using Babeltime.Log;
using Framework.core;
using Framework.core.Components;
using LuaInterface;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Framework.LuaUI.Components
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
            ls.LuaGetField(-1, "DispatchEvent");
            ls.LuaInsert(-2);
            ls.LuaGetGlobal("Event");
            ls.LuaGetField(-1, "New");
            ls.LuaPushString(eventName);
            ls.LuaSafeCall(1, 1, 0, 0);
            ls.LuaInsert(-2);
            ls.LuaPop(1);
            ls.LuaPushBoolean(true);
            ls.LuaSetField(-2, "isBubble");
            ls.LuaSafeCall(2, 0, 0, 0);
        }

    }
}