using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

using JoaoSant0s.ServicePackage.General;

using VTTRPG.CustomServices;

namespace VTTRPG.InternalPopups
{
    public class CreateCharacterSelectionPopup : AnimationPopup
    {
        [Header("PlayerOverview Popup")]
        [SerializeField]
        private TMP_Dropdown dropdown;

        [SerializeField]
        private Button nextButton;

        [SerializeField]
        private Button closeButton;

        private CustomPopupService customPopupService;
        private RPGContentService contentService;

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            contentService = Services.Get<RPGContentService>();
            customPopupService = Services.Get<CustomPopupService>();
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
            var rpgId = contentService.RpgTypes[this.dropdown.value].Id;

            contentService.RequestRPGViewAsset(rpgId, (viewAsset) =>
            {
                customPopupService.ShowCharacterSheetPopup(viewAsset.characterSheetPrefab);
            });
        }

        private void PopulateDropdownOptions()
        {
            this.dropdown.ClearOptions();
            this.dropdown.AddOptions(contentService.RpgTypes.Select(asset => asset.displayName).ToList());
        }

        #endregion
    }
}
