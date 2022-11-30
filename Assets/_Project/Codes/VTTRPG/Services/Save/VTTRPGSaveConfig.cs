using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.CommonWrapper;

namespace VTTRPG.CustomServices
{
    [CreateAssetMenu(fileName = "VTTRPGSaveConfig", menuName = "VTTRPG/Services/VTTRPGSaveConfig")]
    public class VTTRPGSaveConfig : CustomScriptableObject<VTTRPGSaveConfig>
    {
        [Header("VTTRPG Save Config")]

        public string characterSheetsKey;
    }
}
