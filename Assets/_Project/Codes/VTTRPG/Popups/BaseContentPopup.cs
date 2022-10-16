using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Popups;

namespace VTTRPG.CustomPopups
{
    public class BaseContentPopup : Popup
    {
        [Header("Base Content")]

        [SerializeField]
        protected CanvasGroup canvasGroup;

        [SerializeField]
        protected bool autoCloseWhenContentLoaded;

        protected bool isContentVisible = true;

        private LoadingPopup loadingPopup;

        private PopupService popupService;

        protected bool isContentLoaded;

        #region Unity Methods

        protected virtual void Awake()
        {
            isContentLoaded = false;
            this.popupService = Services.Get<PopupService>();
            SetContentInvisible();
        }

        protected virtual void Start()
        {
            if(!autoCloseWhenContentLoaded) return;
            StartCoroutine(WaitToMakeContentVisibleRoutine());
        }

        #endregion

        #region Protected Methods

        protected void SetContentInvisible()
        {
            if (!isContentVisible) return;
            isContentVisible = false;
            canvasGroup.alpha = 0;

            this.loadingPopup = this.popupService.Show<LoadingPopup>((RectTransform)transform);
        }

        protected void SetContentVisible()
        {
            if (isContentVisible) return;

            canvasGroup.alpha = 1;
            this.loadingPopup.Close();
            isContentVisible = true;
        }

        #endregion

        #region Private Methods

        private IEnumerator WaitToMakeContentVisibleRoutine()
        {
            yield return new WaitUntil(() => isContentLoaded);
            SetContentVisible();
        }

        #endregion

    }
}
