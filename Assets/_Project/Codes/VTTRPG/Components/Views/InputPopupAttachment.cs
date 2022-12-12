using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

using JoaoSant0s.ServicePackage.Popups;

using VTTRPG.Inputs;

namespace VTTRPG.Views.Attachment
{
    [RequireComponent(typeof(Popup))]
    public class InputPopupAttachment : MonoBehaviour
    {
        [SerializeField]
        private bool closeInputEnabled = true;
        private InputViewActions inputView;
        private Popup popup;

        private void Awake()
        {
            inputView = new InputViewActions();
            popup = GetComponent<Popup>();
        }

        private void OnEnable()
        {
            inputView.Popups.Enable();
            inputView.Popups.Close.performed += Close;
        }
        private void OnDisable()
        {
            inputView.Popups.Disable();
            inputView.Popups.Close.performed -= Close;
        }
        private void Close(InputAction.CallbackContext context)
        {
            if (!closeInputEnabled) return;
            popup.Close();
        }
    }
}
