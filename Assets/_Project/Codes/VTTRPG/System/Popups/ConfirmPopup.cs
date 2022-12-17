using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using TMPro;

using JoaoSant0s.ServicePackage.Popups;
using VTTRPG.Views.Attachment;

namespace VTTRPG.InternalPopups
{
    [RequireComponent(typeof(InputPopupAttachment))]
    public class ConfirmPopup : Popup
    {
        [Header("Confirm Popup")]
        [SerializeField]
        private Button confirmButton;

        [SerializeField]
        private Button cancelButton;

        [SerializeField]
        private TextMeshProUGUI descriptionLabel;

        private InputPopupAttachment inputAttachment;

        private void Awake()
        {
            this.inputAttachment = GetComponent<InputPopupAttachment>();
        }

        private IEnumerator Start()
        {
            yield return null;
            this.inputAttachment.closeInputEnabled = true;
        }

        #region Public Methods

        public void SetView(string description, UnityAction confirmCallback, UnityAction cancelCallback)
        {
            this.descriptionLabel.text = description;
            this.confirmButton.onClick.AddListener(confirmCallback);
            this.cancelButton.onClick.AddListener(cancelCallback);
        }

        #endregion
    }
}
