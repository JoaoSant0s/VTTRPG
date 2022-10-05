using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

using JoaoSant0s.ServicePackage.Popups;

using VTTRPG.Assets;
using VTTRPG.RPGSystems.Assets;
using JoaoSant0s.ServicePackage.General;

namespace VTTRPG.CustomPopups
{
    public class CreateCharacterSelectionPopup : Popup
    {
        [Header("PlayerOverview Popup")]
        [SerializeField]
        private TMP_Dropdown dropdown;

        [SerializeField]
        private Button nextButton;

        private PopupService popupServices;

        private SystemTypeAsset[] systemTypeAssets;

        #region Unity Methods

        private void Awake()
        {
            systemTypeAssets = ResourcesWrapper.LoadSystemAssets();
            popupServices = Services.Get<PopupService>();
        }

        private void Start()
        {
            PopulateDropdownOptions();
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
            var view = ResourcesWrapper.LoadSystemViewAsset(systemTypeAssets[this.dropdown.value].Id);

            popupServices.Show(view.characterSheetPrefabPopup);
        }

        private void PopulateDropdownOptions()
        {
            this.dropdown.ClearOptions();
            this.dropdown.AddOptions(systemTypeAssets.Select(asset => asset.displayName).ToList());
        }

        #endregion
    }
}
