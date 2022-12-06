using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using VTTRPG.Data;
using VTTRPG.Objects;

namespace VTTRPG.Wrappers
{
    public static class CharacterSheetAdapter
    {
        public static CharacterSheetObject ConvertingDataToObject(CharacterSheetData data)
        {
            var characterObject = new CharacterSheetObject();

            characterObject.rpgId = data.rpgId;
            characterObject.characterName = data.characterName;
            characterObject.intValuesCollections = data.intValuesCollections.ToDictionary(key => key.key, value => value.values);

            return characterObject;
        }

        public static CharacterSheetData ConvertingObjectToData(CharacterSheetObject characterSheetObject)
        {
            var characterData = new CharacterSheetData();

            characterData.rpgId = characterSheetObject.rpgId;
            characterData.characterName = characterSheetObject.characterName;
            characterData.intValuesCollections = characterSheetObject.intValuesCollections.Select(characterSheet => new IntValuesCollectionsSave() { key = characterSheet.Key, values = characterSheet.Value }).ToList();

            return characterData;
        }
    }
}
