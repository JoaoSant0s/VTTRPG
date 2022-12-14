using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DG.Tweening;

namespace Common.TweenAnimations
{
    public class ScaleAnimation : TweenAnimation
    {
        [Header("Scale Parameters")]
        public Vector3 startValue = new Vector3(1, 1, 1);
        public Vector3 endValue = new Vector3(1, 1, 1);
        public float duration = 1;

        #region Override Methods

        public override void CompleteTween()
        {
            base.CompleteTween();
            transform.localScale = startValue;
        }

        protected override Tweener BuildAnimation()
        {
            var animation = transform.DOScale(this.endValue, this.duration);

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
