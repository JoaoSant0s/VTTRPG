using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using VTTRPG.InternalPopups;
using VTTRPG.Views;

namespace VTTRPG.Assets
{
    [CreateAssetMenu(fileName = "RPGViewAsset", menuName = "VTTRPG/Sheet/RPGViewAsset")]
    public class RPGViewAsset : ScriptableObject
    {
        public CharacterSheetPopup characterSheetPrefab;
        public CharacterSheetResumeView characterSheetResumePrefab;
    }
}
