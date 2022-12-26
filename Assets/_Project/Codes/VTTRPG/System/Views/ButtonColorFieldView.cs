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

        #region Protected Override Methods

        public override void AddListeners()
        {
            this.buttonAction.onClick.AddListener(ShowColorPickPopup);
        }

        public override void PopulateValue(ColorValue parameterValue)
        {
            base.PopulateValue(parameterValue);

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

            this.colorPickPopup = PopupWrapper.Show<ColorPickPopup>();
            this.colorPickPopup.Init(fieldViewValue, OnValueUpdated);
            this.colorPickPopup.OnBeforeClose += () => this.colorPickPopup = null;

            OnColorPickPopupAppeared?.Invoke(this.colorPickPopup);
        }

        #endregion
    }
}
