using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

using JoaoSant0s.ServicePackage.Popups;

using VTTRPG.Assets;
using VTTRPG.Data.Assets;
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

        [SerializeField]
        private Button closeButton;

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
            var view = ResourcesWrapper.LoadSystemViewAsset(systemTypeAssets[this.dropdown.value].Id);

            var characterSheet = popupServices.Show(view.characterSheetPrefab);
            characterSheet.Populate();
        }

        private void PopulateDropdownOptions()
        {
            this.dropdown.ClearOptions();
            this.dropdown.AddOptions(systemTypeAssets.Select(asset => asset.displayName).ToList());
        }

        #endregion
    }
}
