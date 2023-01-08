using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.ServicePackage.Popups;
using VTTRPG.CustomServices;

namespace VTTRPG.InternalPopups
{
    public abstract class ContentPopup : AnimationPopup
    {
        [Header("Content Popup")]
        [SerializeField]
        protected CanvasGroup canvasGroup;

        [SerializeField]
        protected bool autoCloseWhenContentLoaded;

        protected bool isContentVisible = true;

        private LoadingPopup loadingPopup;

        protected bool isContentLoaded;

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            isContentLoaded = false;
            SetContentInvisible();
        }

        protected virtual void Start()
        {
            if (!autoCloseWhenContentLoaded) return;
            StartCoroutine(WaitToMakeContentVisibleRoutine());
        }

        #endregion

        #region Protected Methods

        protected void SetContentInvisible()
        {
            if (!isContentVisible) return;
            isContentVisible = false;
            canvasGroup.alpha = 0;

            this.loadingPopup = PopupWrapper.Show<LoadingPopup>((RectTransform)transform);
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
