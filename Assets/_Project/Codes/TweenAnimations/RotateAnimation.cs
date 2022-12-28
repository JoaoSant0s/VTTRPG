using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DG.Tweening;

namespace Common.TweenAnimations
{
    public class RotateAnimation : TweenAnimation
    {
        [Header("Rotate Parameters")]
        public Quaternion startValue = Quaternion.identity;
        public Vector3 endValue = new Vector3(0, 0, 360);
        public float duration = 3;
        public RotateMode mode = RotateMode.Fast;

        #region Override Methods

        public override void CompleteTween()
        {
            base.CompleteTween();
            transform.localRotation = startValue;
        }

        protected override Tweener BuildAnimation()
        {
            var animation = transform.DOLocalRotate(this.endValue, this.duration, this.mode);

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
