using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;
using NaughtyAttributes;

using JoaoSant0s.ServicePackage.General;

using VTTRPG.InternalPopups;
using VTTRPG.CustomServices;
using VTTRPG.Objects;
using VTTRPG.Assets;

namespace VTTRPG.Views
{
    public class DAndD5_0CharacterResumeView : CharacterSheetResumeView
    {
        [Header("D&D 5.0")]

        [SerializeField]
        private RPGViewAsset viewAsset;

        protected override RPGViewAsset ViewAsset => this.viewAsset;
    }
}
