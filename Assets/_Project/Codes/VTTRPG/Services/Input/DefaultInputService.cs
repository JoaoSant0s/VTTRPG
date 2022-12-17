using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Canvases;

namespace VTTRPG.CustomServices
{
    public class DefaultInputService : Service
    {
        private DefaultInputActions defaultInputActions;
        private CanvasService canvasService;

        public override void OnInit()
        {
            defaultInputActions = new DefaultInputActions();
            canvasService = Services.Get<CanvasService>();
        }

        #region Public Methods

        public void EnableActions()
        {
            defaultInputActions.Enable();
        }

        public Vector2 UIScreenPoint()
        {
            return defaultInputActions.UI.Point.ReadValue<Vector2>();
        }

        public bool UIPoint(string canvasId, out Vector2 result)
        {
            var canvasRect = ((RectTransform)canvasService.GetCanvas(canvasId).transform);

            var hit = RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, UIScreenPoint(), null, out Vector2 point);
            result = point;

            return hit;
        }

        #endregion
    }
}
