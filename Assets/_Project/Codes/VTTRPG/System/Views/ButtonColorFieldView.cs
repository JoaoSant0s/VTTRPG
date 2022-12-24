using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using ObjectValues.CoreValues;
using VTTRPG.InternalPopups;
using VTTRPG.CustomServices;
using VTTRPG.Views.Attachment;

namespace VTTRPG.Views
{
    public class ButtonColorFieldView : CustomFieldView<ColorValue>
    {
        public delegate View RequestMainView();
        public RequestMainView OnRequestMainView;

        public event Action<ColorPickPopup> OnColorPickPopupAppeared;    

        [SerializeField]
        private Button buttonAction;

        private ColorPickPopup colorPickPopup;

        #region Protected Override Methods

        public override void AddListeners()
        {
            this.buttonAction.onClick.AddListener(ShowColorPickPopup);
        }

        public override void PopulateValue(ColorValue nameValue) { }

        protected override void ModifyVisual() { }

        #endregion

        #region Private Methods

        private void ShowColorPickPopup()
        {
            if (this.colorPickPopup != null) return;

            var mainView = OnRequestMainView.Invoke();

            this.colorPickPopup = PopupWrapper.Show<ColorPickPopup>();
            this.colorPickPopup.OnBeforeClose += () => this.colorPickPopup = null;

            OnColorPickPopupAppeared?.Invoke(this.colorPickPopup);
        }

        #endregion
    }
}
