using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

using JoaoSant0s.CommonWrapper;
using JoaoSant0s.ServicePackage.Popups;

using VTTRPG.RPGSystems.Assets;
using System.Linq;
using JoaoSant0s.ServicePackage.General;

namespace VTTRPG.Popups
{
    public class PlayerOverviewPopup : Popup
    {
        [Header("PlayerOverview Popup")]
        [SerializeField]
        private Button createButton;

        private PopupService popupService;

        #region Unity Methods

        private void Awake()
        {
            popupService = Services.Get<PopupService>();
        }

        private void Start()
        {
            createButton.onClick.AddListener(() =>
            {
                popupService.Show<CreateCharacterSelectionPopup>();
            });
        }

        #endregion


    }
}
