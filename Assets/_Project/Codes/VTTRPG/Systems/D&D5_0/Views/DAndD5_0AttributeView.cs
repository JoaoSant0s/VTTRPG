using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;
using VTTRPG.Data.Assets;
using VTTRPG.Values;
using VTTRPG.Assets;
using VTTRPG.Wrappers;
using VTTRPG.Systems.Wrappers;

namespace VTTRPG.Views
{
    public class DAndD5_0AttributeView : CustomFieldView<IntValue>
    {
        [Header("TMProFields", order = 1)]

        [SerializeField]
        private TextMeshProUGUI attributeName;

        [SerializeField]
        private TextMeshProUGUI attributeModification;

        [SerializeField]
        private TMP_InputField attributeInputField;

        private IntValue attributeValue;

        private PropertyAsset property;

        public string PropertyId => property.Id;

        #region Override Methods

        public override void AddListeners()
        {
            attributeInputField.onEndEdit.AddListener(OnValueChanged);
        }

        public override void PopulateValue(IntValue value)
        {
            IsValidInput = true;
            this.attributeValue = value;
            ModifyVisual();
        }

        #endregion        

        #region Public Methods

        public void InitView(PropertyAsset property)
        {
            this.property = property;
            attributeName.text = this.property.displayName;
        }

        #endregion

        #region Protected Methods

        protected override void ModifyVisual()
        {
            attributeInputField.text = $"{this.attributeValue.value}";
            ModifyModificatorView(this.attributeValue.value);
        }  

        #endregion

        #region Private Methods

        private void OnValueChanged(string newValue)
        {
            if (!ValidateAttributeValue(newValue, out int newScore)) return;

            attributeValue.ModifyValue(newScore);
            ModifyVisual();
            OnValueUpdated?.Invoke();
        }

        private bool ValidateAttributeValue(string newValue, out int newScore)
        {
            newScore = string.IsNullOrEmpty(newValue) ? 0 : int.Parse(newValue);
            if (newScore < 1 || newScore > 30)
            {
                attributeInputField.text = $"{attributeValue.value}";
                return false;
            }

            return true;
        }

        private void ModifyModificatorView(int score)
        {
            attributeModification.text = UtilWrappers.ApplySignal(RPGMathfs.CalculateModifier(score));
            OnValueUpdated?.Invoke();
        }

        #endregion
    }
}
