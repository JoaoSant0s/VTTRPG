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

        public int GetCharacterSheetIndex(CharacterSheetObject characterSheet)
        {
            return this.characterSheets.IndexOf(characterSheet);
        }

        public void AddCharacterSheet(CharacterSheetObject characterSheet, int indexPosition = -1)
        {
            if (indexPosition < 0)
                this.characterSheets.Add(characterSheet);
            else
                this.characterSheets.Insert(indexPosition, characterSheet);

            SaveData();
            OnCharactersSheetModified?.Invoke();
        }

        public void RemoveCharacterSheet(CharacterSheetObject characterSheet)
        {
            this.characterSheets.Remove(characterSheet);
            SaveData();
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
