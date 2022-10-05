using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.CommonWrapper.Scenes;
using JoaoSant0s.ServicePackage.Popups;
using JoaoSant0s.ServicePackage.General;

using VTTRPG.CustomPopups;

namespace VTTRPG.Scenes
{
    public class MenuScene : MainScene
    {
        private PopupService popupService;

        #region Unity Methods

        private void Awake()
        {
            popupService = Services.Get<PopupService>();
        }

        #endregion

        #region Protected Override Methods

        protected override void Start()
        {
            base.Start();
            popupService.Show<PlayerOverviewPopup>();
        }

        #endregion
    }
}
