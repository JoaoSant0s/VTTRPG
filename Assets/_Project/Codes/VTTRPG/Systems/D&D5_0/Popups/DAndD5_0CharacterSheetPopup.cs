using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using JoaoSant0s.Extensions.Collections;

using VTTRPG.Views;
using VTTRPG.Data.Assets;
using VTTRPG.Assets;
using VTTRPG.Values;

namespace VTTRPG.CustomPopups
{
    public class DAndD5_0CharacterSheetPopup : CharacterSheetPopup
    {
        [Header("Character Sheet Popup", order = 1)]
        [Header("Assets", order = 2)]

        [SerializeField]
        private DAndD5_0AttributeView attributePrefab;

        [SerializeField]
        private SystemTypeAsset systemAsset;

        [Header("References", order = 4)]

        [SerializeField]
        private RectTransform attributesArea;

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
            var attributesModels = ResourcesWrapper.LoadProperties(systemAsset.Id, "Attributes");


            foreach (var property in attributesModels)
            {
                var attributeView = Instantiate(attributePrefab, attributesArea);
                PopulateAttribute(attributeView, property);
            }
        }

        private void PopulateAttribute(DAndD5_0AttributeView attributeView, PropertyAsset property)
        {
            var value = this.characterSheetObject.GetOrCreateIntValue("Attributes", property.Id);

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
