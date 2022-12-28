using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using VTTRPG.Views;

namespace VTTRPG.Assets
{
    [CreateAssetMenu(fileName = "Config", menuName = "VTTRPG/RPGs/D&D5.0/Config")]
    public class DAndD5_0Config : RPGConfig
    {
        [Header("D&D 5.0 Config")]
        public AttributeConfig attributeConfig;
    }

    [Serializable]
    public class AttributeConfig
    {
        [Header("Values")]
        public string attributesKey = "attributes";
        public int attributeDefaultValue = 10;

        [Header("Assets")]
        public DAndD5_0AttributeView attributePrefab;
        public PropertyAsset[] attributes;
    }
}
