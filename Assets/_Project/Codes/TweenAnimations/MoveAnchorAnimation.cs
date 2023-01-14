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

        public Vector2 AnchorValue => (type == AnchorType.Min) ? rectTransform.anchorMin : rectTransform.anchorMax;

        #region Override Methods

        public override void CompleteTween()
        {
            base.CompleteTween();
            if (type == AnchorType.Min)
                rectTransform.anchorMin = startAnchor;
            else
                rectTransform.anchorMax = startAnchor;
        }

        protected override Tweener BuildAnimation()
        {
            var animation = (type == AnchorType.Min) ? BuildAnchorMinAnimation() : BuildAnchorMaxAnimation();

            if (this.tweenerParameters.usingCustomEase)
                animation = animation.SetEase(this.tweenerParameters.curve);
            else
                animation = animation.SetEase(this.tweenerParameters.ease);

            animation = animation.SetRelative(this.tweenerParameters.setRelative);

            if (this.tweenerParameters.isFrom) animation.From();

            return animation;
        }

        #endregion

        #region Private Methods

        private Tweener BuildAnchorMinAnimation() => DOTween.To(() => rectTransform.anchorMin, x => rectTransform.anchorMin = x, endAnchor, duration);

        private Tweener BuildAnchorMaxAnimation() => DOTween.To(() => rectTransform.anchorMax, x => rectTransform.anchorMax = x, endAnchor, duration);

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
