using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NaughtyAttributes;

namespace VTTRPG.Assets
{
    [CreateAssetMenu(fileName = "RPGTypeAsset", menuName = "VTTRPG/Sheet/RPGTypeAsset")]
    public class RPGTypeAsset : ScriptableObject
    {
        [ShowNativeProperty]
        public string Id => name;
        public string displayName;

        [TextArea]
        public string description;
    }
}
