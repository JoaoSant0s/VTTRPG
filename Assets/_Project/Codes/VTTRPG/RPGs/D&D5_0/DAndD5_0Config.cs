using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace VTTRPG.Assets
{
    [CreateAssetMenu(fileName = "Config", menuName = "VTTRPG/RPGs/D&D5.0/Config")]
    public class DAndD5_0Config : RPGConfig
    {
        [Header("D&D 5.0 Config")]
        public string attributesKey = "attributes";
        public int attributeDefaultValue = 10;
        public PropertyAsset[] attributes;
    }
}