using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace VTTRPG.Assets
{
    public abstract class RPGConfig : ScriptableObject
    {
        [Header("RPG Config")]
        public RPGViewAsset viewAsset;
    }
}
