using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using JoaoSant0s.ServicePackage.General;

using VTTRPG.CustomServices;
using VTTRPG.Data.Assets;
using VTTRPG.Objects;

namespace VTTRPG.CustomPopups
{
    public abstract class CharacterSheetPopup : BaseContentPopup
    {
        [Header("Character Sheet Popup")]

        [SerializeField]
        protected SystemTypeAsset systemAsset;

        [SerializeField]
        private Button closeButton;

        [SerializeField]
        private Button saveButton;

        protected CharacterSheetObject characterSheetObject;

        private bool isNewCharacterSheet;
        private VTTRPGSaveService saveService;

        protected abstract void OnPopulateContent();

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            this.isNewCharacterSheet = false;
            this.saveService = Services.Get<VTTRPGSaveService>();
        }

        protected override void Start()
        {
            base.Start();
            this.closeButton.onClick.AddListener(Close);
            this.saveButton.onClick.AddListener(SaveCharacterSheet);
        }

        #endregion

        #region Public Methods

        public void Populate(CharacterSheetObject characterSheetObject)
        {
            this.characterSheetObject = characterSheetObject;
            OnPopulateContent();
        }

        public void Populate()
        {
            this.isNewCharacterSheet = true;
            this.characterSheetObject = new CharacterSheetObject(systemAsset.Id);
            OnPopulateContent();
        }

        #endregion

        #region Private Methods

        private void SaveCharacterSheet()
        {
            if (this.isNewCharacterSheet) this.saveService.characterSheets.Add(this.characterSheetObject);
            this.saveService.SaveData();
            Close();
        }

        #endregion
    }
}
