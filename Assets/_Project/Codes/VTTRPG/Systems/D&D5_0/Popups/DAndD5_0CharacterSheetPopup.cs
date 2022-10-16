using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using VTTRPG.Views;
using VTTRPG.Data.Assets;
using VTTRPG.Assets;

namespace VTTRPG.CustomPopups
{
    public class DAndD5_0CharacterSheetPopup : BaseContentPopup
    {
        [Header("Character Sheet", order = 1)]
        [Header("Assets", order = 2)]

        [SerializeField]
        private DAndD5_0AttributeView attributePrefab;

        [SerializeField]
        private SystemTypeAsset systemAsset;

        [Header("Components", order = 3)]

        [SerializeField]
        private Button closeButton;

        [Header("References", order = 4)]

        [SerializeField]
        private RectTransform attributesArea;

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            closeButton.onClick.AddListener(Close);
        }

        protected override void Start()
        {
            base.Start();
            LoadAttributes();

            StartCoroutine(ContentLoadedRoutine());
        }

        #endregion

        #region Private Methods

        private void LoadAttributes()
        {
            var attributes = ResourcesWrapper.LoadProperties(systemAsset.Id, "Attributes");

            foreach (var property in attributes)
            {
                var attributeView = Instantiate(attributePrefab, attributesArea);
                attributeView.SetPropertyAsset(property);
            }
        }

        private IEnumerator ContentLoadedRoutine()
        {
            yield return null;
            isContentLoaded = true;
        }

        #endregion
    }
}
