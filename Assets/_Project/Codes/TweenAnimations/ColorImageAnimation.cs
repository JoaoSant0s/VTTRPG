using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

namespace Common.TweenAnimations
{
    public class ColorImageAnimation : TweenAnimation
    {
        [Header("Fade Parameters")]
        public Color startValue;

        public Color endValue;
        public float duration;

        [Header("Requirements")]

        [SerializeField]
        private Image image;

        #region Override Methods

        public override void CompleteTween()
        {
            base.CompleteTween();
            image.color = startValue;
        }

        protected override Tweener BuildAnimation()
        {
            var animation = image.DOColor(this.endValue, this.duration);

            if (this.tweenerParameters.usingCustomEase)
                animation = animation.SetEase(this.tweenerParameters.curve);
            else
                animation = animation.SetEase(this.tweenerParameters.ease);

            animation = animation.SetRelative(this.tweenerParameters.setRelative);

            if (this.tweenerParameters.isFrom) animation.From();
            return animation;
        }

        #endregion

    }
}
