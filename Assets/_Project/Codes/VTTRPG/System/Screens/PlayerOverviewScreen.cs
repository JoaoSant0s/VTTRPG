using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using JoaoSant0s.ServicePackage.Screens;
using JoaoSant0s.ServicePackage.General;

using VTTRPG.Views;
using VTTRPG.CustomServices;
using VTTRPG.InternalPopups;
using VTTRPG.Objects;
using VTTRPG.Assets;

namespace VTTRPG.InternalScreens
{
    public class PlayerOverviewScreen : BaseScreen
    {

        [Header("PlayerOverview Popup")]
        [SerializeField]
        private Button createButton;

        [SerializeField]
        private RectTransform charactersResumeContent;

        private CustomSaveService saveService;
        private RPGContentService contentService;

        private LoadingPopup loadingPopup;
        private List<CharacterSheetResumeView> resumeViews;

        #region Unity Methods

        protected override void OnPrepare()
        {
            this.saveService = Services.Get<CustomSaveService>();
            this.contentService = Services.Get<RPGContentService>();
            this.resumeViews = new List<CharacterSheetResumeView>();

            AddListeners();
            LoadCharactersResume();
        }

        protected override void OnRelease()
        {
            this.saveService.OnCharactersSheetModified -= RefreCharactersResume;
        }

        #endregion


        #region Private Methods

        private void LoadCharactersResume()
        {
            this.loadingPopup = PopupWrapper.Show<LoadingPopup>();
            StartCoroutine(LoadCharacterResumeViewsRoutine());
        }

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

            if (this.saveService.characterSheets.Count == 0) TryCloseLoading();
        }

        private void CreateCharacterResumeView(CharacterSheetObject characterSheet, RPGViewAsset viewAsset)
        {
            var characterSheetResume = Instantiate(viewAsset.characterSheetResumePrefab, charactersResumeContent);
            characterSheetResume.Populate(characterSheet);
            this.resumeViews.Add(characterSheetResume);

            if (this.resumeViews.Count != this.saveService.characterSheets.Count) return;

            TryCloseLoading();
        }

        private void TryCloseLoading()
        {
            if (this.loadingPopup == null) return;

            this.loadingPopup.Close();
            this.loadingPopup = null;
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
