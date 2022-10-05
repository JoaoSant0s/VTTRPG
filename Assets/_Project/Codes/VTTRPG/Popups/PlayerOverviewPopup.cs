using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using JoaoSant0s.ServicePackage.Popups;
using JoaoSant0s.ServicePackage.General;

namespace VTTRPG.CustomPopups
{
    public class PlayerOverviewPopup : Popup
    {
        [Header("PlayerOverview Popup")]
        [SerializeField]
        private Button createButton;

        private PopupService popupServices;
        
        
        #region Unity Methods

        private void Awake()
        {
            popupServices = Services.Get<PopupService>();
        }

        private void Start()
        {
            createButton.onClick.AddListener(() =>
            {
                popupServices.Show<CreateCharacterSelectionPopup>();
            });
        }

        #endregion

    }
}
