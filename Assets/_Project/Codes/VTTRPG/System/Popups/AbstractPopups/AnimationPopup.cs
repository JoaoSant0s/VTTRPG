using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using NaughtyAttributes;

using Common.TweenAnimations;
using JoaoSant0s.ServicePackage.Popups;

namespace VTTRPG.InternalPopups
{
    public enum PopupAnimationType
    {
        Scale,
        Fade,
        ScaleAndFade,
        MoveAnchor
    }

    public enum MoveAnchorDirection
    {
        Top,
        Bottom,
        Left,
        Right
    }

    public abstract class AnimationPopup : Popup
    {
        public event Action OnStartCloseAnimation;

        [Header("Animation Popup")]
        [SerializeField]
        private PopupAnimationType openAnimation;

        [SerializeField]
        [ShowIf(nameof(OpenUsingMoveAnchorAnimation))]
        private MoveAnchorDirection openMoveDirectionFrom;
        [Space]

        [SerializeField]
        private PopupAnimationType closeAnimation;

        [SerializeField]
        [ShowIf(nameof(CloseUsingMoveAnchorAnimation))]
        private MoveAnchorDirection closeMoveDirectionTo;

        [Space]

        [Header("Animations")]

        [SerializeField]
        [ShowIf(nameof(UsingScaleAnimation))]
        private ScaleAnimation scaleAnimation;

        [SerializeField]
        [ShowIf(nameof(UsingFadeAnimation))]
        private FadeAnimation fadeAnimation;

        [SerializeField]
        [ShowIf(nameof(UsingMoveAnchorAnimation))]
        private MoveAnchorAnimation moveAnchorMinAnimation;

        [SerializeField]
        [ShowIf(nameof(UsingMoveAnchorAnimation))]
        private MoveAnchorAnimation moveAnchorMaxAnimation;

        private bool UsingScaleAnimation => this.closeAnimation == PopupAnimationType.Scale
                                        || this.openAnimation == PopupAnimationType.Scale
                                        || UsingScaleAndFadeAnimation;

        private bool UsingFadeAnimation => this.closeAnimation == PopupAnimationType.Fade
                                        || this.openAnimation == PopupAnimationType.Fade
                                        || UsingScaleAndFadeAnimation;

        private bool UsingScaleAndFadeAnimation => this.closeAnimation == PopupAnimationType.ScaleAndFade
                                        || this.openAnimation == PopupAnimationType.ScaleAndFade;

        private bool UsingMoveAnchorAnimation => OpenUsingMoveAnchorAnimation || CloseUsingMoveAnchorAnimation;

        private bool OpenUsingMoveAnchorAnimation => this.openAnimation == PopupAnimationType.MoveAnchor;
        private bool CloseUsingMoveAnchorAnimation => this.closeAnimation == PopupAnimationType.MoveAnchor;

        protected bool closing;

        private Dictionary<PopupAnimationType, Action> openAnimationActions;
        private Dictionary<PopupAnimationType, Action<Action>> closeAnimationAction;
        private Dictionary<MoveAnchorDirection, Vector2> moveDirectionValue;

        #region Unity Methods

        protected virtual void Awake()
        {
            BuildMoveDirectionValues();
            BuildOpenAnimationActions();
            BuildCloseAnimationActions();

            Debug.Assert(this.openAnimationActions.ContainsKey(openAnimation), $"Can't find the open animation for the type {openAnimation}");
            this.openAnimationActions[openAnimation]?.Invoke();
        }

        #endregion

        #region Public Methods

        public override void Close()
        {
            Debug.Assert(this.closeAnimationAction.ContainsKey(closeAnimation), $"Can't find the close animation for the type {closeAnimation}");
            if (this.closing) return;
            this.closing = true;

            OnStartCloseAnimation?.Invoke();
            this.closeAnimationAction[closeAnimation]?.Invoke(base.Close);
        }

        public void ForceClose()
        {
            base.Close();
        }

        #endregion

        #region Private Methods

        private void BuildMoveDirectionValues()
        {
            this.moveDirectionValue = new Dictionary<MoveAnchorDirection, Vector2>();
            this.moveDirectionValue.Add(MoveAnchorDirection.Top, Vector2.up);
            this.moveDirectionValue.Add(MoveAnchorDirection.Bottom, Vector2.down);
            this.moveDirectionValue.Add(MoveAnchorDirection.Left, Vector2.left);
            this.moveDirectionValue.Add(MoveAnchorDirection.Right, Vector2.right);
        }

        private void BuildOpenAnimationActions()
        {
            this.openAnimationActions = new Dictionary<PopupAnimationType, Action>();
            if (UsingScaleAnimation) this.openAnimationActions.Add(PopupAnimationType.Scale, scaleAnimation.Run);
            if (UsingFadeAnimation) this.openAnimationActions.Add(PopupAnimationType.Fade, fadeAnimation.Run);
            if (UsingScaleAndFadeAnimation) this.openAnimationActions.Add(PopupAnimationType.ScaleAndFade, () =>
            {
                scaleAnimation.Run();
                fadeAnimation.Run();
            });

            if (OpenUsingMoveAnchorAnimation) this.openAnimationActions.Add(PopupAnimationType.MoveAnchor, () =>
            {
                ChangeAnchorMovementPoints(openMoveDirectionFrom);

                this.moveAnchorMinAnimation.Run();
                this.moveAnchorMaxAnimation.Run();
            });
        }

        private void BuildCloseAnimationActions()
        {
            this.closeAnimationAction = new Dictionary<PopupAnimationType, Action<Action>>();
            if (UsingScaleAnimation) this.closeAnimationAction.Add(PopupAnimationType.Scale, (completeAction) =>
            {
                this.scaleAnimation.SetUsingCustomEase(false);
                this.scaleAnimation.SetIsFrom(true);
                this.scaleAnimation.Run(null, completeAction);
            });
            if (UsingFadeAnimation) this.closeAnimationAction.Add(PopupAnimationType.Fade, (completeAction) =>
            {
                this.fadeAnimation.SetIsFrom(true);
                this.fadeAnimation.Run(null, completeAction);
            });
            if (UsingScaleAndFadeAnimation) this.closeAnimationAction.Add(PopupAnimationType.ScaleAndFade, (completeAction) =>
            {
                this.scaleAnimation.SetUsingCustomEase(false);
                this.scaleAnimation.SetIsFrom(true);
                this.fadeAnimation.SetIsFrom(true);

                this.scaleAnimation.Run(null, completeAction);
                this.fadeAnimation.Run();
            });

            if (CloseUsingMoveAnchorAnimation) this.closeAnimationAction.Add(PopupAnimationType.MoveAnchor, (completeAction) =>
            {
                ChangeAnchorMovementPoints(closeMoveDirectionTo);
                this.moveAnchorMinAnimation.SetIsFrom(true);
                this.moveAnchorMaxAnimation.SetIsFrom(true);

                this.moveAnchorMinAnimation.Run();
                this.moveAnchorMaxAnimation.Run(null, completeAction);
            });
        }

        private void ChangeAnchorMovementPoints(MoveAnchorDirection direction)
        {
            var minAnchorValue = this.moveAnchorMinAnimation.AnchorValue;
            var maxAnchorValue = this.moveAnchorMaxAnimation.AnchorValue;
            var directionValue = moveDirectionValue[direction];

            this.moveAnchorMinAnimation.SetStartAnchor(minAnchorValue + directionValue);
            this.moveAnchorMaxAnimation.SetStartAnchor(maxAnchorValue + directionValue);

            this.moveAnchorMinAnimation.SetEndAnchor(minAnchorValue);
            this.moveAnchorMaxAnimation.SetEndAnchor(maxAnchorValue);
        }

        #endregion
    }
}
