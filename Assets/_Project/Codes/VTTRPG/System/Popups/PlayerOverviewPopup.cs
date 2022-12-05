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
using VTTRPG.Views;

namespace VTTRPG.CustomPopups
{
    public class PlayerOverviewPopup : Popup
    {
        [Header("PlayerOverview Popup")]
        [SerializeField]
        private Button createButton;

        [SerializeField]
        private RectTransform charactersResumeArea;

        [SerializeField]
        private RectTransform charactersResumeContent;

        private CustomSaveService saveService;
        private RPGContentService contentService;

        private LoadingPopup loadingPopup;

        private List<CharacterSheetResumeView> resumeViews;

        #region Unity Methods

        private void Awake()
        {
            this.saveService = Services.Get<CustomSaveService>();
            this.contentService = Services.Get<RPGContentService>();
            this.resumeViews = new List<CharacterSheetResumeView>();
        }

        private void Start()
        {
            AddListeners();
            this.loadingPopup = PopupWrapper.Show<LoadingPopup>();

            StartCoroutine(LoadCharacterResumeViewsRoutine());
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

        private IEnumerator LoadCharacterResumeViewsRoutine()
        {
            yield return null;
            LoadCharacterResumeViews();
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
            var characterSheetResume = Instantiate(viewAsset.characterSheetResumePrefab, charactersResumeContent);
            characterSheetResume.Populate(characterSheet);
            this.resumeViews.Add(characterSheetResume);

            if (this.loadingPopup == null || this.resumeViews.Count != this.saveService.characterSheets.Count) return;

            this.loadingPopup.Close();
        }

        private void RefreCharactersResume()
        {
            ClearViewResumes();
            LoadCharacterResumeViews();
        }

        private void ClearViewResumes()
        {
            for (int i = this.resumeViews.Count - 1; i >= 0; i--)
            {
                GameObject.Destroy(this.resumeViews[i].gameObject);
            }
            this.resumeViews.Clear();
        }

        #endregion

    }
}
