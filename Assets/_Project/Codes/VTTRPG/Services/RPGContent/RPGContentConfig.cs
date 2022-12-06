using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.CommonWrapper;
using VTTRPG.Assets;

namespace VTTRPG.CustomServices
{
    [CreateAssetMenu(fileName = "RPGContentConfig", menuName = "VTTRPG/Services/RPGContentConfig")]
    public class RPGContentConfig : CustomScriptableObject<RPGContentConfig>
    {
        public RPGTypeAsset[] rpgTypes;
    }
}
