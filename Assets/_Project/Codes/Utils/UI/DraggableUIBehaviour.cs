using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

namespace VTTRPG.UIWrappers
{
    public class DraggableUIBehaviour : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private enum DragMode
        {
            Center,
            Relative
        }

        [SerializeField]
        private DragMode dragType;

        protected RectTransform localRectTransform;

        private Vector2 offsetDistance;

        public bool Dragging { get; protected set; }

        private Vector2 ScreenLimits => new Vector2(Screen.width / 2, Screen.height / 2);

        #region Unity Methods

        private void Awake() => localRectTransform = GetComponent<RectTransform>();

        public void OnBeginDrag(PointerEventData eventData)
        {
            Dragging = true;

            var position = eventData.position - ScreenLimits;

            offsetDistance = dragType == DragMode.Relative ? this.localRectTransform.anchoredPosition - position : Vector2.zero;

            this.localRectTransform.anchoredPosition = position + offsetDistance;
        }

        public void OnDrag(PointerEventData eventData)
        {
            SetPosition(eventData.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            SetPosition(eventData.position);
            Dragging = false;
        }

        #endregion

        #region Private Methods

        private void SetPosition(Vector2 position)
        {
            this.localRectTransform.anchoredPosition = position - ScreenLimits + offsetDistance;
        }

        #endregion
    }
}
