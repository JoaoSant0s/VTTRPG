using System.Collections;
using System.Collections.Generic;
using JoaoSant0s.CommonWrapper;
using UnityEngine;
using UnityEngine.UI;

namespace VTTRPG.CustomServices
{
    [CreateAssetMenu(fileName = "VTTRPGSaveConfig", menuName = "VTTRPG/Services/VTTRPGSaveConfig")]
    public class VTTRPGSaveConfig : CustomScriptableObject<VTTRPGSaveConfig>
    {
        [Header("VTTRPG Save Config")]

        public string characterSheetsKey;
    }
}
