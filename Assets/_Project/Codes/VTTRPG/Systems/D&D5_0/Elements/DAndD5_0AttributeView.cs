using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;
using VTTRPG.Data.Assets;

namespace VTTRPG.Views
{
    public class DAndD5_0AttributeView : MonoBehaviour
    {
        [Header("Components", order = 1)]
        [Header("TMProFields", order = 2)]

        [SerializeField]
        private TextMeshProUGUI attributeName;

        [SerializeField]
        private TextMeshProUGUI attributeModification;

        [SerializeField]
        private TMP_InputField attributeInputField;

        [Header("UIFields")]

        [SerializeField]
        private Button runDiceButton;

        private PropertyAsset property;

        #region Unity Methods

        private void Awake()
        {
            runDiceButton.onClick.AddListener(RunDice);
        }

        #endregion        

        #region Public Methods

        public void SetPropertyAsset(PropertyAsset property) => this.property = property;

        #endregion

        #region Private Methods

        private void RunDice()
        {

        }

        #endregion
    }
}
