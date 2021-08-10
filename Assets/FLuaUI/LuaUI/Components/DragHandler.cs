using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FLuaUI.LuaUI.Components
{
    public class DragHandler:MonoBehaviour
    {
        private static DragHandler _current;

        public static DragHandler Current => _current;

        public static bool IsDragging => _current != null;

        private bool _dragging;
        private Vector3 _startPosition;
        private Vector2 _startMousePosition;
        private PointerEventHandler _dropObj;
        private Transform originParent;

        private void Start()
        {
            _startPosition = transform.position;
        }

        public void StartDrag()
        {
            _dragging = true;
            originParent = transform.parent;
            MainGame.Ins.MoveToDraggingStage(gameObject);
            _startMousePosition = EventSystem.current.currentInputModule.input.mousePosition;
            _startPosition = transform.position;
            _current = this;
        }
        
        public PointerEventHandler StopDrag()
        {
            transform.parent = originParent;
            _dragging = false;
            _current = null;
            var obj = _dropObj;
            _dropObj = null;
            return obj;
        }

        public void BackToDragStartPoint()
        {
            transform.position = _startPosition;
        }

        public void SetCurrentDropObj(PointerEventHandler dropObj)
        {
            _dropObj = dropObj;
        }
        
        private void Update()
        {
            if (!_dragging)
            {
                return;
            }
            transform.position = _startPosition + (Vector3)(EventSystem.current.currentInputModule.input.mousePosition - _startMousePosition);
        }
        
    }
}