using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using JoaoSant0s.ServicePackage.Popups;
using JoaoSant0s.ServicePackage.General;

using VTTRPG.CustomServices;
using VTTRPG.Wrappers;

namespace VTTRPG.CustomPopups
{
    public class PlayerOverviewPopup : Popup
    {
        [Header("PlayerOverview Popup")]
        [SerializeField]
        private Button createButton;

        [SerializeField]
        private RectTransform charactersResumeArea;        

        private VTTRPGSaveService saveService;

        #region Unity Methods

        private void Awake()
        {            
            this.saveService = Services.Get<VTTRPGSaveService>();
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
                var view = ResourcesWrapper.LoadSystemViewAsset(characterSheet.systemId);
                Debug.Assert(view != null, $"Can't find a View Asset of the System {characterSheet.systemId}");

                var characterSheetResume = Instantiate(view.characterSheetResumePrefab, charactersResumeArea);
                characterSheetResume.Populate(characterSheet);
            }
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
