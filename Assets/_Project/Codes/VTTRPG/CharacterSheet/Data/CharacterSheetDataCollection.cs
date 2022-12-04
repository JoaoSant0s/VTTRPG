using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace VTTRPG.Data
{
    [Serializable]

    public class CharacterSheetDataCollection
    {
        public List<CharacterSheetData> characterSheets;

        public CharacterSheetDataCollection(List<CharacterSheetData> characterSheetSaves)
        {
            this.characterSheets = characterSheetSaves;
        }

        public CharacterSheetDataCollection()
        {
            this.characterSheets = new List<CharacterSheetData>();
        }
    }
}
