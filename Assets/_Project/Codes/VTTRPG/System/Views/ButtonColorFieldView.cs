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
        [SerializeField]
        private Button buttonAction;

        private ColorPickPopup colorPickPopup;

        private RectTransform popupArea;

        #region Protected Override Methods

        public override void AddListeners()
        {
            this.buttonAction.onClick.AddListener(ShowColorPickPopup);
        }

        public override void PopulateValue(ColorValue nameValue) { }

        protected override void ModifyVisual() { }

        #endregion

        #region Public Methods

        public void SetBasePopupArea(RectTransform parent)
        {
            this.popupArea = parent;
        }

        #endregion

        #region Private Methods

        private void ShowColorPickPopup()
        {
            if (this.colorPickPopup != null) return;
            this.colorPickPopup = PopupWrapper.Show<ColorPickPopup>(this.popupArea);

            this.colorPickPopup.OnBeforeClose += () => this.colorPickPopup = null;
        }

        #endregion
    }
}
