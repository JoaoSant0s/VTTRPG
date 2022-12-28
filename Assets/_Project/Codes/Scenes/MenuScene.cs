using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.CommonWrapper.Scenes;
using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Screens;

using VTTRPG.InternalScreens;

namespace VTTRPG.Scenes
{
    public class MenuScene : MainScene
    {
        #region Unity Methods     

        #endregion

        #region Protected Override Methods

        protected override void Start()
        {
            base.Start();
            Services.Get<ScreenService>().GoToScreen<PlayerOverviewScreen>();
        }

        #endregion
    }
}
