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
    public class DAndD5_0AttributeView : MonoBehaviour
    {
        [Header("Components", order = 1)]
        [Header("TMProFields", order = 2)]

        [SerializeField]
        private TextMeshProUGUI attributeName;

        [SerializeField]
        private TextMeshProUGUI attributeModification;

        [SerializeField]
        private TMP_InputField attributeInputField;

        [Header("UIFields", order = 2)]

        [SerializeField]
        private Button runDiceButton;

        private IntValue attributeValue;

        public PropertyAsset property;

        #region Unity Methods

        private void Awake()
        {
            runDiceButton.onClick.AddListener(RunDice);
            attributeInputField.onEndEdit.AddListener(OnEndAttributeValue);
        }

        #endregion        

        #region Public Methods

        public void InitView(PropertyAsset property)
        {
            this.property = property;
            attributeName.text = this.property.displayName;
        }

        public void PopulateValue(IntValue value)
        {
            this.attributeValue = value;
            UpdateValue(value.value);
        }

        #endregion

        #region Private Methods

        private void UpdateValue(int score)
        {
            attributeInputField.text = $"{score}";
            ModifyModificatorView(score);
        }        

        private void OnEndAttributeValue(string newValue)
        {
            if (!ValidateAttributeValue(newValue, out int newScore)) return;

            attributeValue.ModifyValue(newScore);
            UpdateValue(attributeValue.value);
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
        }

        private void RunDice()
        {

        }

        #endregion
    }
}
