using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using TMPro;

using JoaoSant0s.ServicePackage.Popups;

namespace VTTRPG.InternalPopups
{
    public class ConfirmPopup : Popup
    {
        [Header("Confirm Popup")]
        [SerializeField]
        private Button confirmButton;

        [SerializeField]
        private Button cancelButton;

        [SerializeField]
        private TextMeshProUGUI descriptionLabel;


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
