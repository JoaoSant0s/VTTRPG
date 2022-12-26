using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

using ObjectValues.CoreValues;

using VTTRPG.Assets;
using VTTRPG.UIWrappers;
using VTTRPG.Wrappers;

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

        private PropertyAsset property;

        public string PropertyId => property.Id;

        #region Override Methods

        public override void AddListeners()
        {
            attributeInputField.onEndEdit.AddListener(OnValueChanged);
        }

        public override void PopulateValue(IntValue value)
        {
            base.PopulateValue(value);
            IsValidInput = true;            
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
            attributeInputField.text = $"{this.fieldViewValue.value}";
            ModifyModificatorView(this.fieldViewValue.value);
        }

        #endregion

        #region Private Methods

        private void OnValueChanged(string newValue)
        {
            if (!ValidateAttributeValue(newValue, out int newScore)) return;

            this.fieldViewValue.ModifyValue(newScore);
            ModifyVisual();
            OnValueUpdated?.Invoke();
        }

        private bool ValidateAttributeValue(string newValue, out int newScore)
        {
            newScore = string.IsNullOrEmpty(newValue) ? 0 : int.Parse(newValue);
            if (newScore < 1 || newScore > 30)
            {
                attributeInputField.text = $"{this.fieldViewValue.value}";
                return false;
            }

            return true;
        }

        private void ModifyModificatorView(int score)
        {
            attributeModification.text = ViewAdapters.ApplySignal(RPGMathfs.CalculateModifier(score));
            OnValueUpdated?.Invoke();
        }

        #endregion
    }
}
