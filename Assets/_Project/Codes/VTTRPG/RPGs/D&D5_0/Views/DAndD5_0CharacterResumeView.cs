using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

using JoaoSant0s.ServicePackage.General;

using VTTRPG.CustomPopups;
using VTTRPG.CustomServices;
using VTTRPG.Objects;
using VTTRPG.Assets;

namespace VTTRPG.Views
{
    public class DAndD5_0CharacterResumeView : CharacterSheetResumeView
    {
        [Header("Character Resume View", order = 1)]

        [Header("Assets", order = 2)]
        [SerializeField]
        private RPGViewAsset viewAsset;

        [Header("Views", order = 3)]

        [SerializeField]
        private TextMeshProUGUI characterNameLabel;

        [SerializeField]
        private Button openCharacterSheetButton;

        [SerializeField]
        private Button deleteButton;

        private CharacterSheetObject characterSheet;

        private CustomSaveService saveService;
        private CustomPopupService customPopupService;

        #region Unity Methods

        private void Start()
        {
            this.customPopupService = Services.Get<CustomPopupService>();
            this.saveService = Services.Get<CustomSaveService>();
        }

        private void OnDisable()
        {
            this.characterSheet.characterName.OnChanged -= ModifyCharacterName;
        }

        #endregion

        #region Public Override Methods

        public override void Populate(CharacterSheetObject characterSheet)
        {
            this.characterSheet = characterSheet;
            PopulateVisual();
            AddListeners();
        }

        #endregion

        #region Private Methods

        private void AddListeners()
        {
            this.deleteButton.onClick.AddListener(ShowDeletePopup);
            this.openCharacterSheetButton.onClick.AddListener(() =>
            {
                this.customPopupService.TryShowCharacterSheetPopup(viewAsset.characterSheetPrefab, this.characterSheet);
            });

            this.characterSheet.characterName.OnChanged += ModifyCharacterName;
        }

        private void ShowDeletePopup()
        {
            this.customPopupService.ShowDeleteCharacterPopup(() =>
            {
                ForceClosePopup();
                this.saveService.RemoveCharacterSheet(this.characterSheet);
                this.saveService.SaveData();
            }, (RectTransform)transform);
        }

        private void ForceClosePopup()
        {
            this.customPopupService.TryClosePopupByCondition<CharacterSheetPopup>((popup) => popup.IsSameCharacterSheet(this.characterSheet));
        }

        private void PopulateVisual()
        {
            ModifyCharacterName(characterSheet.characterName.value);
        }

        private void ModifyCharacterName(string name, string previousName = null)
        {
            this.characterNameLabel.text = name;
        }

        #endregion
    }
}