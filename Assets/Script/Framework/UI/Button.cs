using System.Collections.Generic;
using LuaInterface;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Framework.UI
{
    public class Button:MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        private Dictionary<string, List<LuaFunction>> handlerMap = new Dictionary<string, List<LuaFunction>>();
        [NoToLua]
        public void OnPointerClick(PointerEventData eventData)
        {
            DispatchEvent("click");
        }

        [NoToLua]
        public void OnPointerDown(PointerEventData eventData)
        {
            DispatchEvent("down");
        }

        [NoToLua]
        public void OnPointerUp(PointerEventData eventData)
        {
            DispatchEvent("up");
        }

        public void AddEventListener(string eventName, LuaFunction handler)
        {
            List<LuaFunction> dic;
            if (!handlerMap.ContainsKey(eventName))
            {
                dic = new List<LuaFunction>();
                handlerMap[eventName] = dic;
            }
            else
            {
                dic = handlerMap[eventName];
            }

            if (dic.Contains(handler))
            {
                return;
            }

            dic.Add(handler);
        }

        public void DispatchEvent(string eventName)
        {
            if (!handlerMap.ContainsKey(eventName))
            {
                return;
            }

            var handlers = handlerMap[eventName];
            foreach (LuaFunction handler in handlers)
            {
                handler.Call();
            }
        }
    }
}