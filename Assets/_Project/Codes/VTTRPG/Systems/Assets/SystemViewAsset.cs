using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.ServicePackage.Popups;

namespace VTTRPG.RPGSystems.Assets
{
    [CreateAssetMenu(fileName = "SystemViewAsset", menuName = "VTTRPG/Sheet/SystemViewAsset")]
    public class SystemViewAsset : ScriptableObject
    {
        public Popup characterSheetPrefabPopup;
    }
}
