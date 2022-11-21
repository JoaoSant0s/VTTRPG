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

        #region Unity Methods

        private void Awake()
        {
            var canvasService = Services.Get<CanvasService>();
            sceneService = Services.Get<SceneService>();

            Services.Get<PopupService>().Show<VersionPopup>((RectTransform)canvasService.GetCanvas("PopupCanvas").transform);
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
