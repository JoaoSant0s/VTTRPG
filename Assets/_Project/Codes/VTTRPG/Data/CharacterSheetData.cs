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

        public CharacterSheetData() { }
    }

    [Serializable]
    public struct IntValuesCollectionsSave
    {
        public string key;
        public List<IntValue> values;
    }
}
