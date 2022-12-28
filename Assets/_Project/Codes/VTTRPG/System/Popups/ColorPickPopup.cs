using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using TMPro;

using JoaoSant0s.Extensions.Colors;
using JoaoSant0s.ServicePackage.Popups;

using ObjectValues.CoreValues;
using VTTRPG.Views.Attachment;
using VTTRPG.ColorWrappers;

namespace VTTRPG.InternalPopups
{
    [RequireComponent(typeof(FocusViewAttachment))]
    public class ColorPickPopup : Popup
    {
        [Header("Color Pick Popup")]
        [SerializeField]
        private Image lastSelectedColor;

        [SerializeField]
        private Image newSelectedColor;

        [SerializeField]
        private TMP_InputField hexadecimalInputField;

        [Header("Color Components")]

        [SerializeField]
        private ColorComponent redComponent;

        [SerializeField]
        private ColorComponent greenComponent;

        [SerializeField]
        private ColorComponent blueComponent;

        [SerializeField]
        private ColorComponent alphaComponent;

        [Header("Buttons")]

        [SerializeField]
        private Button saveButton;

        [SerializeField]
        private Button closeButton;

        public bool IsToSaveValue { get; private set; }

        private ColorValue colorValue;

        #region Public Methods

        public void Init(ColorValue fieldViewValue)
        {
            this.colorValue = fieldViewValue;

            ModifyVisual();
            AddListeners();
        }

        #endregion        

        #region Protected Methods

        protected override void BeforeClose()
        {
            this.colorValue.OnChanged -= ValueChanged;
        }

        #endregion

        #region Private Methods

        private void ModifyVisual()
        {
            ModifyStartInfo();
            ModifyMutableInfo();
            ModifyComponents();
        }

        private void ModifyStartInfo()
        {
            this.lastSelectedColor.color = colorValue.value;
        }

        private void ModifyMutableInfo()
        {
            this.hexadecimalInputField.text = colorValue.value.ToHex();
            this.newSelectedColor.color = colorValue.value;
        }

        private void ModifyComponents()
        {
            this.redComponent.Value = ColorWrapper.PercentageToNumerical(colorValue.value.r);
            this.greenComponent.Value = ColorWrapper.PercentageToNumerical(colorValue.value.g);
            this.blueComponent.Value = ColorWrapper.PercentageToNumerical(colorValue.value.b);
            this.alphaComponent.Value = ColorWrapper.PercentageToNumerical(colorValue.value.a);
        }

        private void AddListeners()
        {
            this.redComponent.AddListeners();
            this.greenComponent.AddListeners();
            this.blueComponent.AddListeners();
            this.alphaComponent.AddListeners();

            this.closeButton.onClick.AddListener(Close);
            this.saveButton.onClick.AddListener(() =>
            {
                IsToSaveValue = true;
                Close();
            });

            this.redComponent.OnValueChanged += (componentValue) => this.colorValue.ModifyRedComponentValue(componentValue);
            this.greenComponent.OnValueChanged += (componentValue) => this.colorValue.ModifyGreenComponentValue(componentValue);
            this.blueComponent.OnValueChanged += (componentValue) => this.colorValue.ModifyBlueComponentValue(componentValue);
            this.alphaComponent.OnValueChanged += (componentValue) => this.colorValue.ModifyAlphaComponentValue(componentValue);
            this.hexadecimalInputField.onEndEdit.AddListener(OnHexadecimalEnd);
            this.colorValue.OnChanged += ValueChanged;
        }

        private void OnHexadecimalEnd(string hexadecimalColor)
        {
            if (!ColorWrapper.HexToColor(hexadecimalColor, out Color color, this.alphaComponent.Value)) return;
            this.colorValue.ModifyValueIfNew(color);

        }

        private void ValueChanged(Color newColor, Color previousColor)
        {
            ModifyMutableInfo();
            ModifyComponents();
        }

        #endregion
    }

    [Serializable]
    public class ColorComponent
    {
        public event UnityAction<float> OnValueChanged;

        [SerializeField]
        private Slider slider;

        [SerializeField]
        private TMP_InputField inputField;

        private int componentValue;

        private float PercentageValue => ColorWrapper.NumericalToPercentage(Value);

        public int Value
        {
            get => componentValue;
            set
            {
                this.inputField.text = value.ToString();
                this.slider.value = value;
                componentValue = value;
            }
        }

        public void AddListeners()
        {
            this.slider.onValueChanged.AddListener((newValue) =>
            {
                Value = (int)newValue;

                OnValueChanged?.Invoke(PercentageValue);
            });

            this.inputField.onEndEdit.AddListener((newValue) =>
            {
                Value = (int)Mathf.Clamp(int.Parse(newValue), 0, 255);
                OnValueChanged?.Invoke(PercentageValue);
            });
        }
    }
}
