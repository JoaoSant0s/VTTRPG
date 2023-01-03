using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

using JoaoSant0s.ServicePackage.General;

using VTTRPG.CustomServices;
using VTTRPG.Inputs;
using VTTRPG.WrapperPhysics;

namespace VTTRPG.Views.Attachment
{
    public class FocusViewAttachment : MonoBehaviour
    {
        private InputService inputService;
        private InputViewActions inputView;

        public static GameObject FocusedGameObject { get; private set; }
        public static bool HasFocusedView => FocusedGameObject != null;

        #region Unity Methods

        private void Awake()
        {
            this.inputService = Services.Get<InputService>();
            inputView = new InputViewActions();
        }

        private void Start()
        {
            FocusedGameObject = gameObject;
        }

        private void OnDestroy()
        {
            if (FocusedGameObject != gameObject) return;
            CleanFocused();
        }

        private void OnEnable()
        {
            inputView.UI.Enable();
            inputView.UI.Click.performed += TrySetFocusView;
        }

        private void OnDisable()
        {
            inputView.UI.Click.performed -= TrySetFocusView;
            inputView.UI.Disable();
        }

        #endregion    

        #region Private Methods

        private void TrySetFocusView(InputAction.CallbackContext context)
        {
            var point = this.inputService.UIScreenPoint();
            if (!RaycastWrapper.RaycastUIFirst(point, out FocusViewAttachment result))
            {
                CleanFocused();
                return;
            }

            if (result != this) return;
            SetFocusView();
        }

        private void CleanFocused()
        {
            FocusedGameObject = null;
        }

        private void SetFocusView()
        {
            if (FocusedGameObject == gameObject) return;

            ((RectTransform)transform).SetAsLastSibling();
            FocusedGameObject = gameObject;
        }

        #endregion
    }
}
