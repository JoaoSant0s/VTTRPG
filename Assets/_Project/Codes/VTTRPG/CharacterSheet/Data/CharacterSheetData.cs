using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using ObjectValues.CoreValues;

namespace VTTRPG.Data
{
    [Serializable]
    public class CharacterSheetData
    {
        public string rpgId;

        public StringValue characterName;

        public List<IntValuesCollectionsSave> intValuesCollections;

        public CharacterSheetData() { }
    }

    [Serializable]
    public struct IntValuesCollectionsSave
    {
        public string key;
        public List<IntValue> values;
    }
}
