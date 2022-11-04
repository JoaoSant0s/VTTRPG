using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using VTTRPG.Views;
using VTTRPG.Data.Assets;
using VTTRPG.Assets;
using VTTRPG.Configs;

namespace VTTRPG.CustomPopups
{
    public class DAndD5_0CharacterSheetPopup : CharacterSheetPopup
    {
        [Header("Character Sheet Popup", order = 1)]
        [Header("Assets", order = 2)]

        [SerializeField]
        private DAndD5_0AttributeView attributePrefab;

        [Header("References", order = 4)]

        [SerializeField]
        private RectTransform attributesArea;

        private DAndD5_0Config config;

        #region Protected Methods

        protected override void OnPopulateContent()
        {
            LoadAttributes();

            StartCoroutine(ContentLoadedRoutine());
        }

        #endregion

        #region Private Methods

        private void LoadAttributes()
        {
            this.config = ResourcesWrapper.LoadConfig<DAndD5_0Config>(systemAsset.Id);
            var attributesModels = ResourcesWrapper.LoadProperties(systemAsset.Id, this.config.attributes);

            foreach (var property in attributesModels)
            {
                var attributeView = Instantiate(attributePrefab, attributesArea);
                PopulateAttribute(attributeView, property);
            }
        }

        private void PopulateAttribute(DAndD5_0AttributeView attributeView, PropertyAsset property)
        {
            var value = this.characterSheetObject.GetOrCreateIntValue(this.config.attributes, property.Id);

            attributeView.InitView(value, property);
        }

        private IEnumerator ContentLoadedRoutine()
        {
            yield return null;
            isContentLoaded = true;
        }

        #endregion
    }
}
