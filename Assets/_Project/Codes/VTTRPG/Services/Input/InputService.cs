using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Canvases;
using JoaoSant0s.ServicePackage.Popups;

using VTTRPG.Inputs;
using VTTRPG.InternalPopups;
using VTTRPG.Views.Attachment;

namespace VTTRPG.CustomServices
{
    public class InputService : Service
    {
        private DefaultInputActions defaultInputActions;
        private InputViewActions inputView;

        private CanvasService canvasService;
        private PopupService popupServices;

        public override void OnInit()
        {
            this.popupServices = Services.Get<PopupService>();
            this.canvasService = Services.Get<CanvasService>();

            this.defaultInputActions = new DefaultInputActions();
            this.inputView = new InputViewActions();
        }

        #region Public Methods

        public void EnableActions()
        {
            this.defaultInputActions.Enable();
            this.inputView.UI.Enable();

            this.inputView.UI.Exit.performed += ClosePopup;
        }

        public Vector2 UIScreenPoint()
        {
            return this.defaultInputActions.UI.Point.ReadValue<Vector2>();
        }

        public bool UIPoint(string canvasId, out Vector2 result)
        {
            var canvasRect = ((RectTransform)this.canvasService.GetCanvas(canvasId).transform);

            var hit = RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, UIScreenPoint(), null, out Vector2 point);
            result = point;

            return hit;
        }

        #endregion


        #region Private Methods

        private void ClosePopup(InputAction.CallbackContext context)
        {
            if (FocusViewAttachment.HasFocusedView) return;

            var popup = this.popupServices.Show<ConfirmPopup>();
            popup.SetView("Want to exit the VTTRPG?", () =>
            {
                ExitGame();
            }, () =>
            {
                popup.Close();
            });
        }

        private void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        #endregion
    }
}
