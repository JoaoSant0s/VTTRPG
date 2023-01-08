using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DG.Tweening;

namespace Common.TweenAnimations
{
    public enum AnchorType
    {
        Min,
        Max
    }
    public class MoveAnchorAnimation : TweenAnimation
    {
        [Header("Move Anchor Parameters")]

        public AnchorType type;

        public Vector2 startAnchor;
        public Vector2 endAnchor;

        public float duration = 3;

        private RectTransform rectTransform => (RectTransform)transform;

        #region Override Methods

        public override void CompleteTween()
        {
            base.CompleteTween();
            rectTransform.anchorMin = startAnchor;
        }

        protected override Tweener BuildAnimation()
        {
            var animation = (type == AnchorType.Min) ? DOTween.To(() => rectTransform.anchorMin, x => rectTransform.anchorMin = x, endAnchor, duration) : DOTween.To(() => rectTransform.anchorMax, x => rectTransform.anchorMax = x, endAnchor, duration);

            if (this.tweenerParameters.usingCustomEase)
                animation = animation.SetEase(this.tweenerParameters.curve);
            else
                animation = animation.SetEase(this.tweenerParameters.ease);

            animation = animation.SetRelative(this.tweenerParameters.setRelative);

            if (this.tweenerParameters.isFrom) animation.From();

            return animation;
        }

        #endregion

        #region Public Methods

        public void SetStartAnchor(Vector2 anchor)
        {
            this.startAnchor = anchor;
        }

        public void SetEndAnchor(Vector2 anchor)
        {
            this.endAnchor = anchor;
        }

        #endregion
    }
}
