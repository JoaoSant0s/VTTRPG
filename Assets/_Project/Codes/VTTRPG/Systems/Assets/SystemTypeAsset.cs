using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NaughtyAttributes;

namespace VTTRPG.RPGSystems.Assets
{
    [CreateAssetMenu(fileName = "PropertyAsset", menuName = "VTTRPG/Sheet/SystemTypeAsset")]
    public class SystemTypeAsset : ScriptableObject
    {
        [ShowNativeProperty]
        public string Id => name;
        public string displayName;

        [TextArea]
        public string description;
    }
}
