using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using JoaoSant0s.CommonWrapper.Scenes;
using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Scenes;
using NaughtyAttributes;
using JoaoSant0s.ServicePackage.Popups;
using VTTRPG.CustomPopups;
using JoaoSant0s.ServicePackage.Canvases;

namespace VTTRPG.Scenes
{
    public class BootScene : MainScene
    {
        [Header("Boot Scene")]

        [Scene]
        [SerializeField]
        private string nextScene;

        private SceneService sceneService;
        private PopupService popupService;

        #region Unity Methods

        private void Awake()
        {
            sceneService = Services.Get<SceneService>();
            popupService = Services.Get<PopupService>();

            popupService.Show<VersionPopup>();
        }

        #endregion

        #region Protected Override Methods

        protected override void Start()
        {
            base.Start();
            sceneService.Load(nextScene);
        }

        #endregion
    }
}
