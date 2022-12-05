using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using JoaoSant0s.ServicePackage.Popups;
using JoaoSant0s.ServicePackage.General;

using VTTRPG.CustomServices;
using VTTRPG.Wrappers;
using VTTRPG.Objects;
using VTTRPG.Assets;

namespace VTTRPG.CustomPopups
{
    public class PlayerOverviewPopup : Popup
    {
        [Header("PlayerOverview Popup")]
        [SerializeField]
        private Button createButton;

        [SerializeField]
        private RectTransform charactersResumeArea;

        private CustomSaveService saveService;
        private RPGContentService contentService;

        #region Unity Methods

        private void Awake()
        {
            this.saveService = Services.Get<CustomSaveService>();
            this.contentService = Services.Get<RPGContentService>();
        }

        private void Start()
        {
            AddListeners();
            LoadCharacterResumeViews();
        }

        private void OnDisable()
        {
            this.saveService.OnCharactersSheetModified -= RefreCharactersResume;
        }

        #endregion

        #region Private Methods

        private void AddListeners()
        {
            this.createButton.onClick.AddListener(() => PopupWrapper.Show<CreateCharacterSelectionPopup>());
            this.saveService.OnCharactersSheetModified += RefreCharactersResume;
        }

        private void LoadCharacterResumeViews()
        {
            foreach (var characterSheet in this.saveService.characterSheets)
            {
                this.contentService.RequestRPGViewAsset(characterSheet.rpgId, (viewAsset) =>
                {
                    CreateCharacterResumeView(characterSheet, viewAsset);
                });
            }
        }

        private void CreateCharacterResumeView(CharacterSheetObject characterSheet, RPGViewAsset viewAsset)
        {
            var characterSheetResume = Instantiate(viewAsset.characterSheetResumePrefab, charactersResumeArea);
            characterSheetResume.Populate(characterSheet);
        }

        private void RefreCharactersResume()
        {
            ClearChildren();
            LoadCharacterResumeViews();
        }

        private void ClearChildren()
        {
            foreach (Transform child in charactersResumeArea)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        #endregion

    }
}
