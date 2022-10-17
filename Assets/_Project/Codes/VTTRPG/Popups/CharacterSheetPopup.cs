using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using VTTRPG.Objects;

namespace VTTRPG.CustomPopups
{    
    public abstract class CharacterSheetPopup : BaseContentPopup
    {    
        [Header("Character Sheet Popup")]

        [SerializeField]
        private Button closeButton;

        protected CharacterSheetObject characterSheetObject;

        protected abstract void OnPopulateContent();

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            closeButton.onClick.AddListener(Close);
        }

        #endregion

        #region Public Methods

        public void Populate(CharacterSheetObject characterSheetObject = null)
        {
            this.characterSheetObject = characterSheetObject ?? new CharacterSheetObject();
            OnPopulateContent();
        }

        #endregion
    }    
}
