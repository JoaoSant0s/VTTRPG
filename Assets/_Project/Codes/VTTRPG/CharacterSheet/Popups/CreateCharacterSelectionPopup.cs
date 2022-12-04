using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

using JoaoSant0s.ServicePackage.Popups;
using JoaoSant0s.ServicePackage.General;

using VTTRPG.Wrappers;
using VTTRPG.Assets;
using VTTRPG.CustomServices;

namespace VTTRPG.CustomPopups
{
    public class CreateCharacterSelectionPopup : Popup
    {
        [Header("PlayerOverview Popup")]
        [SerializeField]
        private TMP_Dropdown dropdown;

        [SerializeField]
        private Button nextButton;

        [SerializeField]
        private Button closeButton;

        private VTTRPGPopupService customPopupService;

        private RPGTypeAsset[] rpgTypeAssets;

        #region Unity Methods

        private void Awake()
        {
            rpgTypeAssets = ResourcesWrapper.LoadRPGAssets();
            customPopupService = Services.Get<VTTRPGPopupService>();
        }

        private void Start()
        {
            PopulateDropdownOptions();
            closeButton.onClick.AddListener(Close);
            nextButton.onClick.AddListener(() =>
            {
                ShowSelectedCharacterSheet();
                Close();
            });
        }

        #endregion

        #region Private Methods

        private void ShowSelectedCharacterSheet()
        {
            var view = ResourcesWrapper.LoadRPGViewAsset(rpgTypeAssets[this.dropdown.value].Id);
            customPopupService.ShowCharacterSheetPopup(view.characterSheetPrefab);
        }

        private void PopulateDropdownOptions()
        {
            this.dropdown.ClearOptions();
            this.dropdown.AddOptions(rpgTypeAssets.Select(asset => asset.displayName).ToList());
        }

        #endregion
    }
}
