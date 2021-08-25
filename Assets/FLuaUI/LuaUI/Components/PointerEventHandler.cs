using System;
using Babeltime.Log;
using FLuaUI.Components;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FLuaUI.LuaUI.Components
{
    public class PointerEventHandler:GameObjectLuaBinder, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            DispatchEvent("click", eventData, true);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            DispatchEvent("down", eventData, true);
            UpHandler.GetInstance().Down(eventData, this);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            DispatchEvent("up", eventData, true);
        }

        public void OnLongDown(PointerEventData eventData)
        {
            DispatchEvent("longDown", eventData, true);
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (DragHandler.IsDragging)
            {
                DragHandler.Current.SetCurrentDropObj(this);
                DispatchEvent("dragEnter", eventData, false);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (DragHandler.IsDragging)
            {
                DragHandler.Current.SetCurrentDropObj(null);
                DispatchEvent("dragExit", eventData, false);
            }
        }

        private void DispatchEvent(string eventName, PointerEventData eventData, bool isBubble)
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