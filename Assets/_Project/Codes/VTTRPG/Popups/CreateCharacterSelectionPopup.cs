using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

using JoaoSant0s.ServicePackage.Popups;

using VTTRPG.Assets;

namespace VTTRPG.CustomPopups
{
    public class CreateCharacterSelectionPopup : Popup
    {
        [Header("PlayerOverview Popup")]
        [SerializeField]
        private TMP_Dropdown dropdown;

        [SerializeField]
        private Button nextButton;

        #region Unity Methods

        private void Awake()
        {
            PopulateDropdownOptions();
            nextButton.onClick.AddListener(() =>
            {
                Debug.Log(this.dropdown.value);
                Close();
            });
        }

        #endregion

        #region Private Methods

        private void PopulateDropdownOptions()
        {
            this.dropdown.ClearOptions();
            this.dropdown.AddOptions(ResourcesWrapper.LoadSystemAssets().Select(asset => asset.displayName).ToList());
        }

        #endregion
    }
}
