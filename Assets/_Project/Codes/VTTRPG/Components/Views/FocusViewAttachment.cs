using System.Collections;
using System.Collections.Generic;
using JoaoSant0s.CommonWrapper;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using VTTRPG.Inputs;
using VTTRPG.WrapperPhysics;

namespace VTTRPG.Views.Attachment
{
    [RequireComponent(typeof(RectTransform))]
    public class FocusViewAttachment : MonoBehaviour
    {
        private DefaultInputActions defaultInputActions;
        private InputViewActions inputView;

        public static GameObject FocusedGameObject { get; private set; }

        #region Unity Methods

        private void Awake()
        {
            inputView = new InputViewActions();
            defaultInputActions = new DefaultInputActions();
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
            defaultInputActions.UI.Enable();
            inputView.UI.Enable();

            inputView.UI.Click.performed += TrySetFocusView;
        }

        private void OnDisable()
        {
            inputView.UI.Click.performed -= TrySetFocusView;
            defaultInputActions.UI.Disable();

            inputView.UI.Disable();
        }

        #endregion    

        #region Private Methods

        private void TrySetFocusView(InputAction.CallbackContext context)
        {
            var point = defaultInputActions.UI.Point.ReadValue<Vector2>();
            if (!RaycastWrapper.RaycastUIFirst(point, out FocusViewAttachment result)) return;
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
