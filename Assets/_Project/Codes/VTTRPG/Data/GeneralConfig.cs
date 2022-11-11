using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace VTTRPG.Configs
{
    [CreateAssetMenu(fileName = "Config", menuName = "VTTRPG/Systems/General Config")]
    public class GeneralConfig : ScriptableObject
    {
        public string characterNameKey = "character_name";
        public string defaultCharacterName = "Default Name";
    }
}
