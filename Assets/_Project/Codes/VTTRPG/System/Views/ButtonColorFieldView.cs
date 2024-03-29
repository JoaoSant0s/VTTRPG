using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using ObjectValues.CoreValues;
using VTTRPG.InternalPopups;
using VTTRPG.CustomServices;

namespace VTTRPG.Views
{
    public class ButtonColorFieldView : CustomFieldView<ColorValue>
    {
        public event Action<ColorPickPopup> OnColorPickPopupAppeared;

        [SerializeField]
        private Button buttonAction;

        [SerializeField]
        private Image selectedColorImage;

        private ColorPickPopup colorPickPopup;
        private Color startColor;

        #region Unity Methods

        private void OnDestroy()
        {
            this.fieldViewValue.OnChanged -= ValueChanged;
        }

        #endregion

        #region Protected Override Methods

        public override void AddListeners()
        {
            this.buttonAction.onClick.AddListener(ShowColorPickPopup);
        }

        public override void PopulateValue(ColorValue parameterValue)
        {
            base.PopulateValue(parameterValue);

            this.fieldViewValue.OnChanged += ValueChanged;

            ModifyVisual();
        }

        protected override void ModifyVisual()
        {
            this.selectedColorImage.color = fieldViewValue.value;
        }

        #endregion

        #region Private Methods

        private void ShowColorPickPopup()
        {
            if (this.colorPickPopup != null) return;
            this.startColor = fieldViewValue.value;

            this.colorPickPopup = PopupWrapper.Show<ColorPickPopup>();
            this.colorPickPopup.Init(fieldViewValue);
            this.colorPickPopup.OnBeforeClose += BeforeCloseColorPickPopup;

            OnColorPickPopupAppeared?.Invoke(this.colorPickPopup);
        }

        private void BeforeCloseColorPickPopup()
        {
            if (this.colorPickPopup == null) return;

            if (this.colorPickPopup.IsToSaveValue)
            {
                OnValueUpdated?.Invoke();
            }
            else
            {
                fieldViewValue.ModifyValueIfNew(this.startColor);
            }

            this.colorPickPopup = null;
        }

        private void ValueChanged(Color newColor, Color previousColor)
        {
            ModifyVisual();
        }

        #endregion
    }
}
