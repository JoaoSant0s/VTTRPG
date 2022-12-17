using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using NaughtyAttributes;

using JoaoSant0s.CommonWrapper.Scenes;
using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Scenes;

using VTTRPG.CustomServices;
using Common.InternalPopups;

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
            sceneService = Services.Get<SceneService>();
            Services.Get<InputService>().EnableActions();
            PopupWrapper.Show<VersionPopup>();
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
