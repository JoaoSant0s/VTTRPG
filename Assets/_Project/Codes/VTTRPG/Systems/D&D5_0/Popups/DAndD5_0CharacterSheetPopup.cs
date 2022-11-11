using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using VTTRPG.Views;
using VTTRPG.Data.Assets;
using VTTRPG.Assets;
using VTTRPG.Configs;
using TMPro;
using VTTRPG.Values;
using JoaoSant0s.CommonWrapper;

namespace VTTRPG.CustomPopups
{
    public class DAndD5_0CharacterSheetPopup : CharacterSheetPopup
    {
        [Header("Character Sheet Popup", order = 1)]
        [Header("Assets", order = 2)]

        [SerializeField]
        private DAndD5_0AttributeView attributePrefab;

        [Header("Views", order = 3)]

        [SerializeField]
        private CharacterNameView characterNameView;

        [Header("References", order = 4)]

        [SerializeField]
        private RectTransform attributesArea;

        [Header("Input Fields", order = 5)]

        private DAndD5_0Config dAndD5_0Config;

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
            this.dAndD5_0Config = ResourcesWrapper.LoadSystemConfig<DAndD5_0Config>(systemAsset.Id);
            var attributesModels = ResourcesWrapper.LoadProperties(systemAsset.Id, this.dAndD5_0Config.attributesKey);

            foreach (var property in attributesModels)
            {
                var attributeView = Instantiate(attributePrefab, attributesArea);
                attributeView.InitView(property);
                this.attributeViews.Add(attributeView);
            }
        }

        private void PopulateValues()
        {
            this.characterNameView.PopulateValue(this.characterSheetObject.characterName);

            foreach (var attributeView in this.attributeViews)
            {
                var value = this.characterSheetObject.GetOrCreateIntValue(this.dAndD5_0Config.attributesKey, attributeView.PropertyId, this.dAndD5_0Config.attributeDefaultValue);
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
