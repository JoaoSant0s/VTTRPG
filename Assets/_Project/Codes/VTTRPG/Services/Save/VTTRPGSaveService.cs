using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Save;
using JoaoSant0s.CommonWrapper;

using VTTRPG.Objects;
using System;

namespace VTTRPG.CustomServices
{
    public class VTTRPGSaveService : Service
    {
        public event Action OnCharactersSheetModified;
        public List<CharacterSheetObject> characterSheets { get; protected set; }

        private SaveLocalService saveService;
        private VTTRPGSaveConfig config;

        #region Override Methods

        public override void OnInit()
        {
            this.config = VTTRPGSaveConfig.Get();
            this.saveService = Services.Get<SaveLocalService>();
            this.characterSheets = new List<CharacterSheetObject>();
            LoadData();
        }

        #endregion

        #region Public Methods      

        public void SaveData()
        {
            var save = new CharacterSheetCollection(this.characterSheets.Select(sheet => new CharacterSheetSave(sheet)).ToList());
            this.saveService.Set<CharacterSheetCollection>(this.config.characterSheetsKey, save);
            LogInfo();
        }

        public void AddCharacterSheet(CharacterSheetObject newCharacterSheet)
        {
            this.characterSheets.Add(newCharacterSheet);
            OnCharactersSheetModified?.Invoke();
        }

        #endregion

        #region Private Methods

        private void LoadData()
        {
            var save = this.saveService.Get<CharacterSheetCollection>(this.config.characterSheetsKey) ?? new CharacterSheetCollection();
            this.characterSheets = save.characterSheets.Select(sheet => new CharacterSheetObject(sheet)).ToList();
        }

        private void LogInfo()
        {
            foreach (var sheet in this.characterSheets)
            {
                Debug.Log("");
                Debug.Log(sheet.systemId);
                Debug.Log(sheet.characterName);
                foreach (var value in sheet.intValuesCollections)
                {
                    Debug.Log(value.Key);
                    Debugs.Log(value.Value);
                }
            }
        }

        #endregion
    }


}
