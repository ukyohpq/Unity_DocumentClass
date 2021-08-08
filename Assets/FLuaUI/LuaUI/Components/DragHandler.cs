using System;
using Babeltime.Log;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FLuaUI.LuaUI.Components
{
    public class DragHandler:MonoBehaviour
    {
        private bool dragging;
        private Vector3 startPosition;
        private Vector2 startMousePisition;
        public void StartDrag()
        {
            dragging = true;
            startMousePisition = EventSystem.current.currentInputModule.input.mousePosition;
            startPosition = transform.position;
        }

        public void StopDrag()
        {
            dragging = false;
        }

        public void BackToDragStartPoint()
        {
            transform.position = startPosition;
        }
        
        private void Update()
        {
            if (!dragging)
            {
                return;
            }
            transform.position = startPosition + (Vector3)(EventSystem.current.currentInputModule.input.mousePosition - startMousePisition);
            
            
        }
        
    }
}