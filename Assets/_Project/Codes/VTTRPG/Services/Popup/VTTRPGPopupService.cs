using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Popups;

using VTTRPG.CustomPopups;
using VTTRPG.Objects;

namespace VTTRPG.CustomServices
{
    public class VTTRPGPopupService : Service
    {
        public delegate bool CheckCondition<T>(T popup);

        private PopupService popupServices;

        public override void OnInit()
        {
            this.popupServices = Services.Get<PopupService>();
        }

        public void ShowCharacterSheetPopup(CharacterSheetPopup prefab)
        {
            popupServices.Show(prefab).Populate();
        }

        public void TryShowCharacterSheetPopup(CharacterSheetPopup prefab, CharacterSheetObject sheetObject)
        {
            if (IsPopupOpened<CharacterSheetPopup>((popup) => popup.IsSameCharacterSheet(sheetObject))) return;

            var popup = this.popupServices.Show(prefab);
            popup.Populate(sheetObject);
        }

        public bool TryClosePopupByCondition<T>(CheckCondition<T> actionCondition) where T : Popup
        {
            var popups = this.popupServices.GetOpenedPopups<T>();
            var characterSheet = popups.Find(popup => actionCondition(popup));
            if (characterSheet == null) return false;

            characterSheet.Close();
            return true;
        }

        public bool IsPopupOpened<T>(CheckCondition<T> actionCondition = null) where T : Popup
        {
            if (actionCondition == null) return this.popupServices.IsOpened<T>();

            var popups = this.popupServices.GetOpenedPopups<T>();
            return popups.FindIndex(popup => actionCondition(popup)) >= 0;
        }

        public void ShowDeleteCharacterPopup(Action confirmAction, RectTransform parent)
        {
            var popup = this.popupServices.Show<ConfirmPopup>(parent);
            popup.SetView("Want to delete this character sheet?", () =>
            {
                confirmAction?.Invoke();
                popup.Close();
            }, () =>
            {
                popup.Close();
            });
        }




    }
}
