using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace VTTRPG.Assets
{
    [CreateAssetMenu(fileName = "Config", menuName = "VTTRPG/RPGs/General Config")]
    public class CharacterSheetConfig : ScriptableObject
    {
        public string characterNameKey = "character_name";
        public string defaultCharacterName = "Default Name";
    }
}
