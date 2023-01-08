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
    public class ConfirmPopup : AnimationPopup
    {
        [Header("Confirm Popup")]
        [SerializeField]
        private Button confirmButton;

        [SerializeField]
        private Button cancelButton;

        [SerializeField]
        private TextMeshProUGUI descriptionLabel;

        [SerializeField]
        private InputPopupAttachment inputAttachment;

        #region Unity Methods
        private IEnumerator Start()
        {
            yield return null;
            this.inputAttachment.closeInputEnabled = true;
        }

        #endregion

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
