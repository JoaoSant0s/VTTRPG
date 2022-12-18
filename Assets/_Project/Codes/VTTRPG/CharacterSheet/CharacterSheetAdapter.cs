using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using VTTRPG.Data;
using VTTRPG.Objects;
using VTTRPG.ValueAdapters;
using ObjectValues.CoreValues;
using JoaoSant0s.ServicePackage.General;
using VTTRPG.CustomServices;

namespace VTTRPG.Wrappers
{
    public static class CharacterSheetAdapter
    {
        public static CharacterSheetObject ConvertingDataToObject(CharacterSheetData data)
        {
            var characterObject = new CharacterSheetObject();

            characterObject.rpgId = data.rpgId;
            characterObject.characterName = data.characterName;
            characterObject.characterResume = data.characterResume ?? characterObject.characterResume;
            characterObject.sheetColor = ValueAdapter.ToColorValue(data.sheetColor) ?? characterObject.sheetColor;
            characterObject.intValuesCollections = data.intValuesCollections.ToDictionary(key => key.key, value => value.values);

            return characterObject;
        }

        public static CharacterSheetData ConvertingObjectToData(CharacterSheetObject characterSheetObject)
        {
            var characterData = new CharacterSheetData();

            characterData.rpgId = characterSheetObject.rpgId;
            characterData.characterName = characterSheetObject.characterName;
            characterData.characterResume = characterSheetObject.characterResume;
            characterData.sheetColor = ValueAdapter.ToStringValue(characterSheetObject.sheetColor); ;
            characterData.intValuesCollections = characterSheetObject.intValuesCollections.Select(characterSheet => new IntValuesCollectionsSave() { key = characterSheet.Key, values = characterSheet.Value }).ToList();

            return characterData;
        }
    }
}
