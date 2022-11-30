using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using ObjectValues.CoreValues;
using VTTRPG.Objects;

namespace VTTRPG.Data
{
    [System.Serializable]
    public class CharacterSheetData
    {
        public string systemId;

        public StringValue characterName;

        public List<IntValuesCollectionsSave> intValuesCollections;

        public CharacterSheetData(CharacterSheetObject sheet)
        {
            this.systemId = sheet.systemId;
            this.characterName = sheet.characterName;
            this.intValuesCollections = sheet.intValuesCollections.Select(characterSheet => new IntValuesCollectionsSave() { key = characterSheet.Key, values = characterSheet.Value }).ToList();
        }
    }

    [Serializable]
    public struct IntValuesCollectionsSave
    {
        public string key;
        public List<IntValue> values;
    }
}
