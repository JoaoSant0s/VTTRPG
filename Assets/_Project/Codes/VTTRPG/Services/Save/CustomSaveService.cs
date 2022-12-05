using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Save;

using VTTRPG.Data;
using VTTRPG.Objects;
using VTTRPG.Wrappers;

namespace VTTRPG.CustomServices
{
    public class CustomSaveService : Service
    {
        public event Action OnCharactersSheetModified;
        public List<CharacterSheetObject> characterSheets { get; protected set; }

        private SaveLocalService saveService;
        private CustomSaveConfig config;

        #region Override Methods

        public override void OnInit()
        {
            this.config = CustomSaveConfig.Get();
            this.saveService = Services.Get<SaveLocalService>();
            this.characterSheets = new List<CharacterSheetObject>();
            LoadData();
        }

        #endregion

        #region Public Methods      

        public void SaveData()
        {
            var save = new CharacterSheetDataCollection(this.characterSheets.Select(sheet => CharacterSheetAdapter.ConvertingObjectToData(sheet)).ToList());
            this.saveService.Set<CharacterSheetDataCollection>(this.config.characterSheetsKey, save);
        }

        public void AddCharacterSheet(CharacterSheetObject characterSheet)
        {
            this.characterSheets.Add(characterSheet);
            OnCharactersSheetModified?.Invoke();
        }

        public void RemoveCharacterSheet(CharacterSheetObject characterSheet)
        {
            this.characterSheets.Remove(characterSheet);
            OnCharactersSheetModified?.Invoke();
        }

        #endregion

        #region Private Methods

        private void LoadData()
        {
            var save = this.saveService.Get<CharacterSheetDataCollection>(this.config.characterSheetsKey) ?? new CharacterSheetDataCollection();

            this.characterSheets = save.characterSheets.Select(sheet => CharacterSheetAdapter.ConvertingDataToObject(sheet)).ToList();
        }

        #endregion
    }


}
