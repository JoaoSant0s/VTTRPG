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
        private DAndD5_0Config rpgConfig;

        [Header("Views", order = 3)]
        [SerializeField]
        private Image sheetBackground;

        [SerializeField]
        private StringFieldView stringFieldView;

        [SerializeField]
        private ButtonColorFieldView buttonColorFieldView;

        [Header("References", order = 4)]

        [SerializeField]
        private RectTransform attributesArea;

        [Header("Input Fields", order = 5)]
        private List<DAndD5_0AttributeView> attributeViews;

        private AttributeConfig attributeConfig;

        #region Protected Methods

        protected override void OnPopulateContent()
        {
            this.attributeConfig = rpgConfig.attributeConfig;

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

            foreach (var property in this.attributeConfig.attributes)
            {
                var attributeView = Instantiate(this.attributeConfig.attributePrefab, attributesArea);
                attributeView.InitView(property);
                this.attributeViews.Add(attributeView);
            }
        }

        private void PopulateValues()
        {
            this.stringFieldView.PopulateValue(this.characterSheetObject.characterName);
            this.buttonColorFieldView.SetBasePopupArea((RectTransform)transform);
            this.buttonColorFieldView.PopulateValue(this.characterSheetObject.sheetColor);
            this.sheetBackground.color = this.characterSheetObject.sheetColor.value;

            foreach (var attributeView in this.attributeViews)
            {
                var value = this.characterSheetObject.GetOrCreateIntValue(this.attributeConfig.attributesKey, attributeView.PropertyId, this.attributeConfig.attributeDefaultValue);
                attributeView.PopulateValue(value);
            }
        }

        private void AddListeners()
        {
            this.stringFieldView.AddListeners();
            this.buttonColorFieldView.AddListeners();
            this.stringFieldView.OnValueUpdated += SaveCharacterSheet;

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
