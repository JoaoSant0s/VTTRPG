using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace VTTRPG.Configs
{
    [CreateAssetMenu(fileName = "Config", menuName = "VTTRPG/Systems/D&D5.0/Config")]
    public class DAndD5_0Config : SystemConfig
    {
        public string attributesKey = "attributes";
        public int attributeDefaultValue = 10;
    }
}
