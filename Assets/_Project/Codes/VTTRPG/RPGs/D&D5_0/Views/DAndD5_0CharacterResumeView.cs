using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;
using NaughtyAttributes;

using JoaoSant0s.ServicePackage.General;

using VTTRPG.InternalPopups;
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
        private Image sheetBackground;

        [SerializeField]
        private TextMeshProUGUI characterNameLabel;

        [BoxGroup("Buttons")]
        [SerializeField]
        private Button openCharacterSheetButton;

        [BoxGroup("Buttons")]
        [SerializeField]
        private Button deleteButton;

        [BoxGroup("Buttons")]
        [SerializeField]
        private Button dubplicateButton;

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
            this.characterSheet.sheetColor.OnChanged -= ModifySheetColor;
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
            this.dubplicateButton.onClick.AddListener(DuplicateCharacterSheet);
            this.deleteButton.onClick.AddListener(ShowDeletePopup);
            this.openCharacterSheetButton.onClick.AddListener(() =>
            {
                this.customPopupService.TryShowCharacterSheetPopup(viewAsset.characterSheetPrefab, this.characterSheet);
            });

            this.characterSheet.characterName.OnChanged += ModifyCharacterName;
            this.characterSheet.sheetColor.OnChanged += ModifySheetColor;
        }

        private void DuplicateCharacterSheet()
        {
            var nextIndex = this.saveService.GetCharacterSheetIndex(this.characterSheet) + 1;
            this.saveService.AddCharacterSheet(this.characterSheet.MakeCopy(), nextIndex);
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

        #endregion
    }
}
