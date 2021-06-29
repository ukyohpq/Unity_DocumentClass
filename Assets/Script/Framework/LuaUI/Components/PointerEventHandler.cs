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
    public class PointerEventHandler:GameObjectLuaBinder, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            DispatchEvent("click", eventData, true);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            DispatchEvent("down", eventData, true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            DispatchEvent("up", eventData, true);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            DispatchEvent("BeginDrag", eventData, false);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            DispatchEvent("OnEndDrag", eventData, false);
        }

        public void OnDrag(PointerEventData eventData)
        {
            DispatchEvent("OnDrag", eventData, false);
        }

        public void OnDrop(PointerEventData eventData)
        {
            DispatchEvent("OnDrop", eventData, false);
        }

        public void DispatchEvent(string eventName, PointerEventData eventData, bool isBubble)
        {
//            BTLog.Error("DispatchEvent:{0} pressPosition:{1} position:{2} delta:{3} dragging:{4}", eventName, eventData.pressPosition, eventData.position, eventData.delta, eventData.dragging);
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
            ls.LuaPushBoolean(isBubble);
            ls.LuaSetField(-2, "isBubble");
            ls.LuaSafeCall(2, 0, 0, 0);
        }

    }
}