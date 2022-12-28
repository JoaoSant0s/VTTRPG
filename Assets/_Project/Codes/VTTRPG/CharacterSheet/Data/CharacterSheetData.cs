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

        #region Character Sheet Header Configs

        public StringValue characterName;
        public StringValue characterResume;
        public StringValue sheetColor;

        #endregion

        #region Character Sheet Body Configs

        public List<IntValuesCollectionsSave> intValuesCollections;

        #endregion

        public CharacterSheetData() { }
    }

    [Serializable]
    public struct IntValuesCollectionsSave
    {
        public string key;
        public List<IntValue> values;
    }
}
