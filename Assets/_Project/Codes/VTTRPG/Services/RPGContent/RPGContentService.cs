using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using JoaoSant0s.ServicePackage.General;
using VTTRPG.Assets;

namespace VTTRPG.CustomServices
{
    public class RPGContentService : Service
    {
        private RPGContentConfig config;
        public RPGTypeAsset[] RpgTypes => this.config.rpgTypes;

        private CharacterSheetConfig generalConfig;

        #region Override Methods

        public override void OnInit()
        {
            this.config = RPGContentConfig.Get();
        }

        #endregion

        #region Public Methods

        public CharacterSheetConfig GetCharacterSheetConfig()
        {
            if (generalConfig == null) generalConfig = Resources.Load<CharacterSheetConfig>($"VTTRPG/CharacterSheetConfig");

            return generalConfig;
        }

        #endregion
    }
}
