using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

namespace Common.TweenAnimations
{
    public class FadeAnimation : TweenAnimation
    {
        [Header("Fade Parameters")]
        [Range(0f, 1f)]
        public float startValue = 0;

        [Range(0f, 1f)]
        public float endValue = 1;
        public float duration = 1;

        [Header("Requirements")]

        [SerializeField]
        private CanvasGroup canvasGroup;

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
