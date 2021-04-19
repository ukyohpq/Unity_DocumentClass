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

            public void Destroy()
            {
                self = null;
                handler.Dispose();
                handler = null;
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
                    cb.Destroy();
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
            var ls = MainGame.Ins.LuaState;
            foreach (var cb in handlers)
            {
                ls.LuaGetRef(cb.handler.GetReference());
                var nArgs = 0;
                if (cb.self != null)
                {
                    ls.LuaGetRef(cb.self.GetReference());
                    nArgs++;
                }
                ls.LuaSafeCall(nArgs, 0, 0, 0);
            }
        }

        private void OnDestroy()
        {
            foreach (var kv in handlerMap)
            {
                var handlers = kv.Value;
                foreach (var cb in handlers)
                {
                    cb.Destroy();
                }
            }
            
            handlerMap = null;
        }
    }
}