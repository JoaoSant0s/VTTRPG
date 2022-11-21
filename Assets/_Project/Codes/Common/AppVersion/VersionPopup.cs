using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

using JoaoSant0s.ServicePackage.Popups;

namespace VTTRPG.CustomPopups
{
    public class VersionPopup : Popup
    {
        [SerializeField]
        private TextMeshProUGUI versionLabel;


        #region Unity Methods

        private void Start()
        {
            this.versionLabel.text = $"Version: {Application.version}";
        }

        #endregion
    }
}
