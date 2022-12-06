using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Popups;

namespace VTTRPG.CustomServices
{
    public static class PopupWrapper
    {
        public static T Show<T>() where T : Popup
        {
            return Services.Get<PopupService>().Show<T>();
        }

        public static T Show<T>(RectTransform parent) where T : Popup
        {
            return Services.Get<PopupService>().Show<T>(parent);
        }
    }
}
