using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

using ObjectValues.CoreValues;

namespace VTTRPG.Views
{
    public class MultiStringFieldView : CustomFieldView<StringValue>
    {
        [Header("Multi String FieldView")]

        [SerializeField]
        private TMP_InputField multiStringInputField;

        #region Override Methods

        public override void AddListeners()
        {
            this.multiStringInputField.onEndEdit.AddListener(OnValueChanged);
        }

        public override void PopulateValue(StringValue fieldViewValue)
        {
            base.PopulateValue(fieldViewValue);

            ModifyVisual();
        }

        protected override void ModifyVisual()
        {
            this.multiStringInputField.text = this.fieldViewValue.value;
        }

        #endregion


        #region Private Methods

        private void OnValueChanged(string name)
        {
            this.fieldViewValue.ModifyValue(name);
            OnValueUpdated?.Invoke();
        }

        #endregion
    }
}
