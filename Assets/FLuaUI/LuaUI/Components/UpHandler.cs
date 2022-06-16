using FLua.Log;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FLuaUI.LuaUI.Components
{
    public class UpHandler
    {
        private static UpHandler ins;

        public static UpHandler GetInstance()
        {
            if (ins == null)
            {
                ins = new UpHandler();
            }
            return ins;
        }

        private float downTime;
        private bool isDown;
        private bool canSendLong;
        private PointerEventData eventData;
        private PointerEventHandler pointHandler;
        private UpHandler(){}

        public void Down(PointerEventData eventData, PointerEventHandler handler)
        {
            downTime = Time.realtimeSinceStartup;
            isDown = true;
            canSendLong = true;
            this.eventData = eventData;
            pointHandler = handler;
        }
        public void Update()
        {
            if (!isDown)
            {
                return;
            }

            eventData.position = Input.mousePosition;
            if (Input.GetMouseButtonUp((int)eventData.button))
            {
                pointHandler.OnPointerUp(eventData);
                isDown = false;
            }
            
            if (canSendLong && Time.realtimeSinceStartup - downTime > 0.5)
            {
                canSendLong = false;
                pointHandler.OnLongDown(eventData);
            }
        }
    }
    
    
}