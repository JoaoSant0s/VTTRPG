using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NaughtyAttributes;

namespace VTTRPG.Data.Assets
{
    [CreateAssetMenu(fileName = "PropertyAsset", menuName = "VTTRPG/Properties/PropertyAsset")]
    public class PropertyAsset : ScriptableObject
    {
        [ShowNativeProperty]
        public string Id => name;
        public string displayName;
    }
}
