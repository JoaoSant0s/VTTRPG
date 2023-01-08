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
        private MoveAnchorDirection openMoveDirection;
        [Space]

        [SerializeField]
        private PopupAnimationType closeAnimation;

        [SerializeField]
        [ShowIf(nameof(CloseUsingMoveAnchorAnimation))]
        private MoveAnchorDirection closeMoveDirection;

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

        private bool CloseUsingMoveAnchorAnimation => this.closeAnimation == PopupAnimationType.MoveAnchor;

        private bool OpenUsingMoveAnchorAnimation => this.openAnimation == PopupAnimationType.MoveAnchor;

        protected bool closing;

        private Dictionary<PopupAnimationType, Action> openAnimationActions;
        private Dictionary<PopupAnimationType, Action<Action>> closeAnimationAction;


        #region Unity Methods

        protected virtual void Awake()
        {
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
        }

        private void BuildCloseAnimationActions()
        {
            this.closeAnimationAction = new Dictionary<PopupAnimationType, Action<Action>>();
            if (UsingScaleAnimation) this.closeAnimationAction.Add(PopupAnimationType.Scale, (completeAction) =>
            {
                scaleAnimation.SetUsingCustomEase(false);
                scaleAnimation.SetIsFrom(true);
                scaleAnimation.Run(null, completeAction);
            });
            if (UsingFadeAnimation) this.closeAnimationAction.Add(PopupAnimationType.Fade, (completeAction) =>
            {
                fadeAnimation.SetIsFrom(true);
                fadeAnimation.Run(null, completeAction);
            });
            if (UsingScaleAndFadeAnimation) this.closeAnimationAction.Add(PopupAnimationType.ScaleAndFade, (completeAction) =>
            {
                scaleAnimation.SetUsingCustomEase(false);
                scaleAnimation.SetIsFrom(true);
                fadeAnimation.SetIsFrom(true);

                scaleAnimation.Run(null, completeAction);
                fadeAnimation.Run();
            });
        }

        #endregion
    }
}
