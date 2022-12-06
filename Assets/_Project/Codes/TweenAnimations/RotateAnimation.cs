using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DG.Tweening;
using NaughtyAttributes;

namespace Common.TweenAnimations
{
    public class RotateAnimation : TweenAnimation
    {
        [Header("Rotate Parameters")]
        public Quaternion startValue = Quaternion.identity;
        public Vector3 endValue = new Vector3(0, 0, 360);
        public float duration = 3;
        public RotateMode mode = RotateMode.Fast;

        [Header("Animation Parameters")]
        public Ease ease = Ease.Linear;
        public bool setRelative = false;
        public bool isFrom = false;

        [Header("Sequence Parameters")]
        public int loops = -1;
        [ShowIf(nameof(HasLoop))]
        [AllowNesting]
        public LoopType loopType = LoopType.Restart;
        public float startInterval = -1;
        public float endInterval = -1;

        private bool HasLoop => this.loops != 0;

        #region Public Methods

        public override void Run(Action startCallback = null, Action endCallback = null)
        {
            CompleteTween();

            this.sequence = DOTween.Sequence();
            if (startCallback != null) this.sequence.PrependCallback(() => startCallback?.Invoke());
            if (this.startInterval >= 0) this.sequence.PrependInterval(this.startInterval);
            var animation = transform.DOLocalRotate(this.endValue, this.duration, this.mode).SetEase(this.ease).SetRelative(this.setRelative);
            if (isFrom) animation.From();
            this.sequence.Append(animation);

            if (this.endInterval >= 0) this.sequence.AppendInterval(this.endInterval);
            if (endCallback != null) this.sequence.AppendCallback(() => endCallback?.Invoke());

            if (HasLoop) this.sequence.SetLoops(this.loops, loopType);

            this.sequence.Play();
        }

        public override void CompleteTween()
        {
            base.CompleteTween();
            transform.localRotation = startValue;
        }

        #endregion
    }
}
