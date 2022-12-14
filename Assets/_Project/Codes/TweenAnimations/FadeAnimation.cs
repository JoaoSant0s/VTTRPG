using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

namespace Common.TweenAnimations
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FadeAnimation : TweenAnimation
    {
        [Header("Fade Parameters")]
        [Range(0f, 1f)]
        public float startValue = 0;

        [Range(0f, 1f)]
        public float endValue = 1;
        public float duration = 1;

        private CanvasGroup canvasGroup;

        #region Unity Methods

        private void Awake()
        {
            this.canvasGroup = GetComponent<CanvasGroup>();
        }

        #endregion

        #region Override Methods

        public override void CompleteTween()
        {
            base.CompleteTween();
            canvasGroup.alpha = startValue;
        }

        protected override Tweener BuildAnimation()
        {
            var animation = this.canvasGroup.DOFade(this.endValue, this.duration);

            if (tweenerParameters.usingCustomEase)
                animation = animation.SetEase(tweenerParameters.curve);
            else
                animation = animation.SetEase(tweenerParameters.ease);

            animation = animation.SetRelative(tweenerParameters.setRelative);

            if (tweenerParameters.isFrom) animation.From();
            return animation;
        }

        #endregion
    }
}
