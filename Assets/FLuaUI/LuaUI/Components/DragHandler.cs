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
        public void StartDrag()
        {
            _dragging = true;
            _startMousePosition = EventSystem.current.currentInputModule.input.mousePosition;
            _startPosition = transform.position;
            _current = this;
            ActiveCanvasGroup(false);
        }

        private void ActiveCanvasGroup(bool active)
        {
            var cg = gameObject.GetComponent<CanvasGroup>();
            if (cg == null)
            {
                cg = gameObject.AddComponent<CanvasGroup>();
            }
            cg.interactable = active;
            cg.blocksRaycasts = active;
        }
        public bool StopDrag()
        {
            _dragging = false;
            _current = null;
            ActiveCanvasGroup(true);
            if (_dropObj != null)
            {
                var evt = new PointerEventData(EventSystem.current);
                _dropObj.Drop(evt);
                _dropObj = null;
                return true;
            }

            return false;
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