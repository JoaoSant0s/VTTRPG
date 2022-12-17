using System.Collections;
using System.Collections.Generic;
using JoaoSant0s.ServicePackage.General;
using UnityEngine;
using UnityEngine.InputSystem;
using VTTRPG.CustomServices;
using VTTRPG.Inputs;
using VTTRPG.WrapperPhysics;

namespace VTTRPG.Views.Attachment
{
    [RequireComponent(typeof(RectTransform))]
    public class FocusViewAttachment : MonoBehaviour
    {
        private DefaultInputService defaultInputService;

        private InputViewActions inputView;

        public static GameObject FocusedGameObject { get; private set; }

        #region Unity Methods

        private void Awake()
        {
            this.defaultInputService = Services.Get<DefaultInputService>();
            inputView = new InputViewActions();
        }

        private void Start()
        {
            FocusedGameObject = gameObject;
        }

        private void OnDestroy()
        {
            if (FocusedGameObject != gameObject) return;
            FocusedGameObject = null;
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
            var point = this.defaultInputService.UIScreenPoint();
            if (!RaycastWrapper.RaycastUIFirst(point, out FocusViewAttachment result))
            {
                FocusedGameObject = null;
                return;
            }
            if (result != this) return;

            SetFocusView();
        }

        #endregion

        #region Public Methods

        public void SetFocusView()
        {
            ((RectTransform)transform).SetAsLastSibling();
            FocusedGameObject = gameObject;
        }

        #endregion
    }
}
