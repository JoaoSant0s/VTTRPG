using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using TMPro;
using UnityEngine.UI;

using Common.TweenAnimations;
using JoaoSant0s.ServicePackage.General;

using VTTRPG.Assets;
using VTTRPG.CustomServices;
using VTTRPG.InternalPopups;
using VTTRPG.Objects;
using VTTRPG.Wrappers;

namespace VTTRPG.Views
{
    public abstract class CharacterSheetResumeView : MonoBehaviour
    {
        [Header("Character Resume View", order = 1)]

        [SerializeField]
        private Image sheetBackground;

        [SerializeField]
        private TextMeshProUGUI characterNameLabel;

        [Header("Buttons", order = 2)]

        [SerializeField]
        private Button openCharacterSheetButton;

        [SerializeField]
        private Button deleteButton;

        [SerializeField]
        private Button dubplicateButton;

        [SerializeField]
        private ScaleAnimation scaleAnimation;

        protected abstract RPGViewAsset ViewAsset { get; }

        protected CharacterSheetObject characterSheet;
        protected CustomSaveService saveService;
        protected CustomPopupService customPopupService;

        #region Unity Methods

        private void Start()
        {
            this.customPopupService = Services.Get<CustomPopupService>();
            this.saveService = Services.Get<CustomSaveService>();
        }

        private void OnDisable()
        {
            this.characterSheet.characterName.OnChanged -= ModifyCharacterName;
            this.characterSheet.sheetColor.OnChanged -= ModifySheetColor;
        }

        #endregion

        #region Public Methods

        public void Populate(CharacterSheetObject characterSheet)
        {
            this.characterSheet = characterSheet;

            PopulateVisual();
            AddListeners();
            TryShowAnimation();
        }

        #endregion

        #region Private Methods     

        private void PopulateVisual()
        {
            ModifyCharacterName(characterSheet.characterName.value);
            ModifySheetColor(characterSheet.sheetColor.value, Color.black);
        }

        private void ModifyCharacterName(string name, string previousName = null)
        {
            this.characterNameLabel.text = name;
        }

        private void ModifySheetColor(Color color, Color previousColor)
        {
            this.sheetBackground.color = color;
        }

        private void AddListeners()
        {
            this.dubplicateButton.onClick.AddListener(DuplicateCharacterSheet);
            this.deleteButton.onClick.AddListener(ShowDeletePopup);
            this.openCharacterSheetButton.onClick.AddListener(() =>
            {
                this.customPopupService.TryShowCharacterSheetPopup(ViewAsset.characterSheetPrefab, this.characterSheet);
            });

            this.characterSheet.characterName.OnChanged += ModifyCharacterName;
            this.characterSheet.sheetColor.OnChanged += ModifySheetColor;
        }

        private void TryShowAnimation()
        {
            if (!CharacterSheetAdapter.RemoveClonedCharacterSheets(this.characterSheet)) return;

            this.scaleAnimation.Run();
        }

        private void DuplicateCharacterSheet()
        {
            var nextIndex = this.saveService.GetCharacterSheetIndex(this.characterSheet) + 1;
            var copy = this.characterSheet.MakeCopy();
            CharacterSheetAdapter.AddClonedCharacterSheets(copy);
            this.saveService.AddCharacterSheet(copy, nextIndex);
        }

        private void ShowDeletePopup()
        {
            this.customPopupService.ShowDeleteCharacterPopup(() =>
            {
                ForceClosePopup();
                this.saveService.RemoveCharacterSheet(this.characterSheet);
            }, (RectTransform)transform);
        }

        private void ForceClosePopup()
        {
            this.customPopupService.TryClosePopupByCondition<CharacterSheetPopup>((popup) => popup.IsSameCharacterSheet(this.characterSheet));
        }

        #endregion
    }
}
