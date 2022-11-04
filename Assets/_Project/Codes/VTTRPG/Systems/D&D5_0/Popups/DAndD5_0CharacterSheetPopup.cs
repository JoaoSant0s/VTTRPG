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

        [Header("References", order = 4)]

        [SerializeField]
        private RectTransform attributesArea;

        [Header("Input Fields", order = 5)]
        [SerializeField]
        private TMP_InputField characterNameInputField;

        private DAndD5_0Config dAndD5_0Config;
        private GeneralConfig generalConfing;

        #region Protected Methods

        protected override void OnPopulateContent()
        {
            LoadAttributes();
            AddListeners();

            StartCoroutine(ContentLoadedRoutine());
        }

        #endregion

        #region Private Methods

        private void LoadAttributes()
        {
            this.dAndD5_0Config = ResourcesWrapper.LoadSystemConfig<DAndD5_0Config>(systemAsset.Id);
            this.generalConfing = ResourcesWrapper.LoadGeneralConfig();
            var attributesModels = ResourcesWrapper.LoadProperties(systemAsset.Id, this.dAndD5_0Config.attributesKey);

            foreach (var property in attributesModels)
            {
                var attributeView = Instantiate(attributePrefab, attributesArea);
                PopulateAttribute(attributeView, property);
            }
        }

        private void AddListeners()
        {
            characterNameInputField.onEndEdit.AddListener(OnCharacterNameEnded);
        }

        private void PopulateAttribute(DAndD5_0AttributeView attributeView, PropertyAsset property)
        {
            var value = this.characterSheetObject.GetOrCreateIntValue(this.dAndD5_0Config.attributesKey, property.Id);

            attributeView.InitView(value, property);
        }

        private IEnumerator ContentLoadedRoutine()
        {
            yield return null;
            isContentLoaded = true;
        }

        private void OnCharacterNameEnded(string newValue)
        {
            if (this.characterSheetObject.characterName == null) this.characterSheetObject.characterName = new StringValue(generalConfing.characterNameKey, newValue);
            this.characterSheetObject.characterName.ModifyValue(newValue);
        }

        #endregion
    }
}
