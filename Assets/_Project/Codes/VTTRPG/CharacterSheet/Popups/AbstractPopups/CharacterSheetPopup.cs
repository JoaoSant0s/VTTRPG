using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using JoaoSant0s.ServicePackage.General;

using VTTRPG.CustomServices;
using VTTRPG.Assets;
using VTTRPG.Objects;
using VTTRPG.Views;
using VTTRPG.Views.Attachment;

namespace VTTRPG.InternalPopups
{
    [RequireComponent(typeof(FocusViewAttachment))]
    public abstract class CharacterSheetPopup : ContentPopup
    {
        [Header("Character Sheet Popup")]

        [SerializeField]
        protected RPGTypeAsset rpgAsset;

        [SerializeField]
        private Button closeButton;

        protected CharacterSheetObject characterSheetObject;

        private CustomSaveService saveService;

        protected abstract void OnPopulateContent();

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            this.saveService = Services.Get<CustomSaveService>();
        }

        protected override void Start()
        {
            base.Start();
            this.closeButton.onClick.AddListener(Close);
        }

        #endregion

        #region Public Methods

        public void Populate(CharacterSheetObject characterSheetObject)
        {
            this.characterSheetObject = characterSheetObject;
            OnPopulateContent();
        }

        public bool IsSameCharacterSheet(CharacterSheetObject characterSheetObject)
        {
            return this.characterSheetObject == characterSheetObject;
        }

        public void Populate()
        {
            this.characterSheetObject = new CharacterSheetObject(rpgAsset.Id);
            this.saveService.AddCharacterSheet(this.characterSheetObject);
            OnPopulateContent();
        }

        #endregion

        #region Protected Methods

        protected void SaveCharacterSheet()
        {
            this.saveService.SaveData();
        }

        #endregion
    }
}
