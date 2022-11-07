using System.Collections;
using System.Collections.Generic;
using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Popups;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VTTRPG.Assets;
using VTTRPG.Objects;

namespace VTTRPG.Views
{
    public class DAndD5_0CharacterResumeView : CharacterSheetResumeView
    {
        [SerializeField]
        private TextMeshProUGUI characterNameLabel;

        [SerializeField]
        private Button openCharacterSheetButton;

        private PopupService popupServices;

        private CharacterSheetObject characterSheet;

        #region Unity Methods

        private void Start()
        {
            popupServices = Services.Get<PopupService>();
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

            this.openCharacterSheetButton.onClick.AddListener(() =>
            {
                var characterSheet = popupServices.Show(view.characterSheetPrefab);
                characterSheet.Populate(this.characterSheet);
            });

            this.characterSheet.characterName.OnChanged += ModifyCharacterName;
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
