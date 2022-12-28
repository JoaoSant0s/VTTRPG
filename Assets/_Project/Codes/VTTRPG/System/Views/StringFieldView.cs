using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using NaughtyAttributes;
using TMPro;

using ObjectValues.CoreValues;

namespace VTTRPG.Views
{
    public class StringFieldView : CustomFieldView<StringValue>
    {
        [Header("String FieldView")]
        [SerializeField]
        private TMP_InputField stringInputField;

        [SerializeField]
        private bool hasErrorLabel = true;

        [ShowIf(nameof(hasErrorLabel))]
        [SerializeField]
        private TextMeshProUGUI errorLabel;

        #region Protected Override Methods

        public override void AddListeners()
        {
            this.stringInputField.onEndEdit.AddListener(OnValueChanged);
        }

        public override void PopulateValue(StringValue fieldViewValue)
        {
            base.PopulateValue(fieldViewValue);

            MakeValid(true);
            ModifyVisual();
        }

        protected override void ModifyVisual()
        {
            this.stringInputField.text = this.fieldViewValue.value;
        }

        #endregion

        #region Private Methods

        private void OnValueChanged(string name)
        {
            if (!ValidateNameValue(name, out string newName)) return;

            this.fieldViewValue.ModifyValue(newName);
            OnValueUpdated?.Invoke();
            MakeValid(true);
        }

        private bool ValidateNameValue(string value, out string newValue)
        {
            newValue = value;
            if (hasErrorLabel && string.IsNullOrEmpty(value))
            {
                errorLabel.text = "The character name must be not empty";
                MakeValid(false);

                return false;
            }

            return true;
        }

        private void MakeValid(bool value)
        {
            if (!hasErrorLabel) return;
            IsValidInput = value;
            errorLabel.gameObject.SetActive(!value);
        }

        #endregion
    }
}
