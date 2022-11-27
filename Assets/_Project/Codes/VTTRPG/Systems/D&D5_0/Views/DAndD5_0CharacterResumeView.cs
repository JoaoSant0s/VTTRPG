using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Popups;

using VTTRPG.Assets;
using VTTRPG.CustomPopups;
using VTTRPG.CustomServices;
using VTTRPG.Objects;

namespace VTTRPG.Views
{
    public class DAndD5_0CharacterResumeView : CharacterSheetResumeView
    {
        [SerializeField]
        private TextMeshProUGUI characterNameLabel;

        [SerializeField]
        private Button openCharacterSheetButton;

        [SerializeField]
        private Button deleteButton;

        private PopupService popupServices;

        private CharacterSheetObject characterSheet;

        private VTTRPGSaveService saveService;

        #region Unity Methods

        private void Start()
        {
            this.popupServices = Services.Get<PopupService>();
            this.saveService = Services.Get<VTTRPGSaveService>();
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
            var view = ResourcesWrapper.LoadSystemViewAsset(this.characterSheet.systemId);

            this.deleteButton.onClick.AddListener(ShowDeletePopup);
            this.openCharacterSheetButton.onClick.AddListener(() =>
            {
                if (IsCharacterSheetOpened()) return;
                var popup = this.popupServices.Show(view.characterSheetPrefab);
                popup.Populate(this.characterSheet);
            });

            this.characterSheet.characterName.OnChanged += ModifyCharacterName;
        }

        private bool IsCharacterSheetOpened()
        {
            var popups = this.popupServices.GetOpenedPopups<CharacterSheetPopup>();
            return popups.FindIndex(popup => popup.IsSameCharacterSheet(this.characterSheet)) >= 0;
        }

        private void ShowDeletePopup()
        {
            var popup = this.popupServices.Show<ConfirmPopup>((RectTransform)transform);
            popup.SetView($"Want to delete this character sheet?", () =>
            {
                ForceClosePopup();
                this.saveService.RemoveCharacterSheet(this.characterSheet);
                this.saveService.SaveData();
                popup.Close();
            }, () =>
            {
                popup.Close();
            });
        }

        private void ForceClosePopup()
        {
            var popups = this.popupServices.GetOpenedPopups<CharacterSheetPopup>();
            var characterSheet = popups.Find(popup => popup.IsSameCharacterSheet(this.characterSheet));
            if (characterSheet == null) return;

            characterSheet.Close();
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
