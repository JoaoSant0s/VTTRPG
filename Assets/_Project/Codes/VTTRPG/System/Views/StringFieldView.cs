using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

using ObjectValues.CoreValues;

namespace VTTRPG.Views
{
    public class StringFieldView : CustomFieldView<StringValue>
    {
        [Header("TMProFields", order = 2)]

        [SerializeField]
        private TMP_InputField characterNameInputField;

        [SerializeField]
        private TextMeshProUGUI errorLabel;

        private StringValue nameValue;

        #region Protected Override Methods

        public override void AddListeners()
        {
            this.characterNameInputField.onEndEdit.AddListener(OnValueChanged);
        }

        public override void PopulateValue(StringValue nameValue)
        {
            this.nameValue = nameValue;
            MakeValid(true);
            ModifyVisual();
        }

        protected override void ModifyVisual()
        {
            this.characterNameInputField.text = this.nameValue.value;
        }

        #endregion

        #region Private Methods

        private void OnValueChanged(string name)
        {
            if (!ValidateNameValue(name, out string newName)) return;

            this.nameValue.ModifyValue(newName);
            OnValueUpdated?.Invoke();
            MakeValid(true);
        }

        private bool ValidateNameValue(string value, out string newValue)
        {
            newValue = value;
            if (string.IsNullOrEmpty(value))
            {
                errorLabel.text = "The character name must be not empty";
                MakeValid(false);

                return false;
            }

            return true;
        }

        private void MakeValid(bool value)
        {
            IsValidInput = value;
            errorLabel.gameObject.SetActive(!value);
        }

        #endregion
    }
}
