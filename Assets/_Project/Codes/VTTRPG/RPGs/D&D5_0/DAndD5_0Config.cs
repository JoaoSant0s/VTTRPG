using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace VTTRPG.Assets
{
    [CreateAssetMenu(fileName = "Config", menuName = "VTTRPG/RPGs/D&D5.0/Config")]
    public class DAndD5_0Config : RPGConfig
    {
        public string attributesKey = "attributes";
        public int attributeDefaultValue = 10;
    }
}
