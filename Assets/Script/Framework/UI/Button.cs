using System;
using System.Collections.Generic;
using Babeltime.Log;
using LuaInterface;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Framework.UI
{
    public class Button:MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        class LuaCallback
        {
            public LuaTable self;
            public LuaFunction handler;

            public LuaCallback(LuaTable self, LuaFunction handler)
            {
                this.self = self;
                this.handler = handler;
            }
            
            public override bool Equals(object obj)
            {
                var target = obj as LuaCallback;
                if (target == null)
                {
                    return false;
                }

                return self == target.self && handler == target.handler;
            }
        }
        private Dictionary<string, List<LuaCallback>> handlerMap = new Dictionary<string, List<LuaCallback>>();
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

        public void AddEventListener(string eventName, LuaTable self, LuaFunction handler)
        {
            if (handler == null)
            {
                BTLog.Error("can not AddEventListener with null handler");
                return;
            }
            List<LuaCallback> dic;
            if (!handlerMap.ContainsKey(eventName))
            {
                dic = new List<LuaCallback>();
                handlerMap[eventName] = dic;
            }
            else
            {
                dic = handlerMap[eventName];
            }

            foreach (LuaCallback cb in dic)
            {
                if (cb.self == self && cb.handler == handler)
                {
                    return;
                }
            }

            dic.Add(new LuaCallback(self, handler));
        }

        public void RemoveEventListener(string eventName, LuaTable self, LuaFunction handler )
        {
            if (handler == null)
            {
                return;
            }
            List<LuaCallback> dic;
            if (!handlerMap.ContainsKey(eventName))
            {
                return;
            }

            dic = handlerMap[eventName];
            foreach (LuaCallback cb in dic)
            {
                if (cb.self == self && cb.handler == handler)
                {
                    dic.Remove(cb);
                    return;
                }
            }
        }
        public void DispatchEvent(string eventName)
        {
            if (!handlerMap.ContainsKey(eventName))
            {
                return;
            }

            var handlers = handlerMap[eventName];
            foreach (var cb in handlers)
            {
                if (cb.self != null)
                {
                    cb.handler.Call(cb.self);
                }
                else
                {
                    cb.handler.Call();
                }
            }
        }

        private void OnDestroy()
        {
            this.handlerMap = null;
        }
    }
}