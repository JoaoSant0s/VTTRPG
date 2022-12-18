using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using VTTRPG.Views;
using VTTRPG.Assets;

namespace VTTRPG.InternalPopups
{
    public class DAndD5_0CharacterSheetPopup : CharacterSheetPopup
    {
        [Header("Character Sheet Popup", order = 1)]
        [Header("Assets", order = 2)]

        [SerializeField]
        private DAndD5_0AttributeView attributePrefab;

        [SerializeField]
        private DAndD5_0Config rpgConfig;
        
        [Header("Views", order = 3)]
        [SerializeField]
        private Image sheetBackground;

        [SerializeField]
        private CharacterNameView characterNameView;

        [Header("References", order = 4)]

        [SerializeField]
        private RectTransform attributesArea;

        [Header("Input Fields", order = 5)]

        private List<DAndD5_0AttributeView> attributeViews;

        #region Protected Methods

        protected override void OnPopulateContent()
        {
            LoadAttributes();
            PopulateValues();
            AddListeners();

            StartCoroutine(ContentLoadedRoutine());
        }

        #endregion

        #region Private Methods

        private void LoadAttributes()
        {
            this.attributeViews = new List<DAndD5_0AttributeView>();

            foreach (var property in this.rpgConfig.attributes)
            {
                var attributeView = Instantiate(attributePrefab, attributesArea);
                attributeView.InitView(property);
                this.attributeViews.Add(attributeView);
            }
        }

        private void PopulateValues()
        {
            this.characterNameView.PopulateValue(this.characterSheetObject.characterName);
            this.sheetBackground.color = this.characterSheetObject.sheetColor.value;

            foreach (var attributeView in this.attributeViews)
            {
                var value = this.characterSheetObject.GetOrCreateIntValue(this.rpgConfig.attributesKey, attributeView.PropertyId, this.rpgConfig.attributeDefaultValue);
                attributeView.PopulateValue(value);
            }
        }

        private void AddListeners()
        {
            this.characterNameView.AddListeners();
            this.characterNameView.OnValueUpdated += SaveCharacterSheet;
            foreach (var attributeView in this.attributeViews)
            {
                attributeView.AddListeners();
                attributeView.OnValueUpdated += SaveCharacterSheet;
            }
        }

        private IEnumerator ContentLoadedRoutine()
        {
            yield return null;
            isContentLoaded = true;
        }

        #endregion
    }
}
