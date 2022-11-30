using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.CommonWrapper.Scenes;

using VTTRPG.CustomPopups;
using VTTRPG.CustomServices;

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
            PopupWrapper.Show<PlayerOverviewPopup>();
        }

        #endregion
    }
}
