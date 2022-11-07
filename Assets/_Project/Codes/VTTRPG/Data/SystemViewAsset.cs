using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using VTTRPG.CustomPopups;
using VTTRPG.Views;

namespace VTTRPG.Data.Assets
{
    [CreateAssetMenu(fileName = "SystemViewAsset", menuName = "VTTRPG/Sheet/SystemViewAsset")]
    public class SystemViewAsset : ScriptableObject
    {
        public CharacterSheetPopup characterSheetPrefab;
        public CharacterSheetResumeView characterSheetResumePrefab;
    }
}
