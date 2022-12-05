using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using JoaoSant0s.ServicePackage.General;
using VTTRPG.Assets;
using System;

namespace VTTRPG.CustomServices
{
    public class RPGContentService : Service
    {
        public RPGTypeAsset[] RpgTypes => this.config.rpgTypes;

        private CharacterSheetConfig generalConfig;
        private RPGContentConfig config;
        private Dictionary<string, RPGConfig> rpgConfigCollection;

        #region Override Methods

        public override void OnInit()
        {
            this.config = RPGContentConfig.Get();
            this.rpgConfigCollection = new Dictionary<string, RPGConfig>();
        }

        #endregion

        #region Public Methods

        public CharacterSheetConfig GetCharacterSheetConfig()
        {
            if (generalConfig == null) generalConfig = Resources.Load<CharacterSheetConfig>($"VTTRPG/CharacterSheetConfig");

            return generalConfig;
        }

        public void RequestRPGViewAsset(string rpgId, Action<RPGViewAsset> resultAction)
        {
            if (!rpgConfigCollection.ContainsKey(rpgId))
            {
                var path = $"VTTRPG/RPGs/{rpgId}/RPGConfig";

                StartCoroutine(LoadContentRoutine<RPGConfig>(path, (configAsset) =>
                {
                    if (!rpgConfigCollection.ContainsKey(rpgId)) rpgConfigCollection.Add(rpgId, configAsset);
                    resultAction?.Invoke(configAsset.viewAsset);
                }));

                return;
            }

            resultAction?.Invoke(rpgConfigCollection[rpgId].viewAsset);
        }

        #endregion

        #region Private Methods

        private IEnumerator LoadContentRoutine<T>(string path, Action<T> resultAction) where T : UnityEngine.Object
        {
            var request = Resources.LoadAsync<T>(path);
            yield return new WaitUntil(() => request.isDone);
            resultAction?.Invoke((T)request.asset);
        }

        #endregion
    }
}
