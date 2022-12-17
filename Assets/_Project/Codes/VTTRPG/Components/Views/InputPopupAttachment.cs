using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

using JoaoSant0s.ServicePackage.Popups;

using VTTRPG.Inputs;

namespace VTTRPG.Views.Attachment
{
    [RequireComponent(typeof(Popup), typeof(FocusViewAttachment))]
    public class InputPopupAttachment : MonoBehaviour
    {
        public bool closeInputEnabled = true;
        private InputViewActions inputView;
        private Popup popup;
        private FocusViewAttachment focusView;

        #region Unity Methods

        private void Awake()
        {
            inputView = new InputViewActions();
            popup = GetComponent<Popup>();
            focusView = GetComponent<FocusViewAttachment>();
        }

        private void OnEnable()
        {
            inputView.Popups.Enable();

            inputView.Popups.Close.performed += Close;
        }
        private void OnDisable()
        {
            inputView.Popups.Close.performed -= Close;

            inputView.Popups.Disable();
        }

        #endregion

        #region Private Methods
        private void Close(InputAction.CallbackContext context)
        {
            if (!closeInputEnabled) return;
            if (FocusViewAttachment.FocusedGameObject != gameObject) return;
            popup.Close();
        }

        #endregion
    }
}
