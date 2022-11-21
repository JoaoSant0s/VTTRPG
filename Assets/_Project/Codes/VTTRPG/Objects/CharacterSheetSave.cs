using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using VTTRPG.Values;

namespace VTTRPG.Objects
{
    [System.Serializable]
    public class CharacterSheetSave
    {
        public string systemId;

        public StringValue characterName;

        public List<IntValuesCollectionsSave> intValuesCollections;

        public CharacterSheetSave(CharacterSheetObject sheet)
        {
            this.systemId = sheet.systemId;
            this.characterName = sheet.characterName;
            this.intValuesCollections = sheet.intValuesCollections.Select(characterSheet => new IntValuesCollectionsSave() { key = characterSheet.Key, values = characterSheet.Value }).ToList();
        }
    }
    
    [System.Serializable]
    public struct IntValuesCollectionsSave
    {
        public string key;
        public List<IntValue> values;
    }

    [System.Serializable]
    public class CharacterSheetCollection
    {
        public List<CharacterSheetSave> characterSheets;

        public CharacterSheetCollection(List<CharacterSheetSave> characterSheetSaves)
        {
            this.characterSheets = characterSheetSaves;
        }

        public CharacterSheetCollection()
        {
            this.characterSheets = new List<CharacterSheetSave>();
        }
    }
}
