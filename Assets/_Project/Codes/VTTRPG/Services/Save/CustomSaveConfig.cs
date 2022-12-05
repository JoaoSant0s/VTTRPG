using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.CommonWrapper;

namespace VTTRPG.CustomServices
{
    [CreateAssetMenu(fileName = "CustomSaveConfig", menuName = "VTTRPG/Services/CustomSaveConfig")]
    public class CustomSaveConfig : CustomScriptableObject<CustomSaveConfig>
    {
        [Header("VTTRPG Save Config")]

        public string characterSheetsKey;
    }
}
