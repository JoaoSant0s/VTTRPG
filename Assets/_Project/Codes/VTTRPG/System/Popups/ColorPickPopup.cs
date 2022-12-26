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


        private ColorValue colorValue;
        private bool isToSaveValue;
        private Color startColor;

        private Action onValueUpdated;

        #region Public Methods

        public void Init(ColorValue fieldViewValue, Action OnValueUpdated)
        {
            this.colorValue = fieldViewValue;
            this.onValueUpdated = OnValueUpdated;
            this.startColor = fieldViewValue.value;

            ModifyVisual();
            AddListeners();
        }

        #endregion

        #region Protected Methods

        protected override void BeforeClose()
        {
            if (this.isToSaveValue)
            {
                this.onValueUpdated?.Invoke();
                return;
            }

            colorValue.ModifyValueIfNew(this.startColor);
        }

        #endregion

        #region Private Methods

        private void ModifyVisual()
        {
            this.hexadecimalInputField.text = colorValue.value.ToHex();
            this.lastSelectedColor.color = colorValue.value;
            this.newSelectedColor.color = colorValue.value;

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
                this.isToSaveValue = true;
                Close();
            });

            //this.redComponent.OnValueChanged += 

            this.colorValue.OnChanged += (newColor, previousColor) => {};
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
                OnValueChanged?.Invoke(Value);
            });

            this.inputField.onEndEdit.AddListener((newValue) =>
            {
                Value = (int)Mathf.Clamp(int.Parse(newValue), 0, 255);
                OnValueChanged?.Invoke(Value);
            });
        }
    }
}
