using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using NaughtyAttributes;

namespace Common.TweenAnimations
{
    public abstract class Tween : MonoBehaviour
    {
        [Header("Control")]
        public bool autoRun = true;

        public TweenerParameters tweenerParameters;
        public SequenceParameters sequenceParameters;

        protected Sequence sequence;

        #region Unity Methods

        protected virtual void Start()
        {
            if (!autoRun) return;

            Run();
        }
        protected virtual void OnDestroy()
        {
            CompleteTween();
        }

        #endregion

        #region Abstract Methods

        public abstract void Run(Action startCallback = null, Action endCallback = null);

        #endregion

        #region Public Virtual Methods

        public virtual void CompleteTween()
        {
            if (this.sequence == null) return;

            this.sequence.Complete();
            this.sequence.Kill();
        }

        #endregion

        #region Public Methods

        public void SetUsingCustomEase(bool usingCustomEase)
        {
            this.tweenerParameters.usingCustomEase = usingCustomEase;
        }

        public void SetIsFrom(bool isFrom)
        {
            this.tweenerParameters.isFrom = isFrom;
        }

        public void Run()
        {
            Run(null, null);
        }

        #endregion
    }

    [Serializable]
    public class TweenerParameters
    {
        public bool usingCustomEase;

        [ShowIf(nameof(usingCustomEase))]
        [AllowNesting]
        public AnimationCurve curve;

        [HideIf(nameof(usingCustomEase))]
        [AllowNesting]
        public Ease ease = Ease.Linear;
        public bool setRelative = false;
        public bool isFrom = false;
    }

    [Serializable]
    public class SequenceParameters
    {
        public int loops = 0;
        [ShowIf(nameof(HasLoop))]
        [AllowNesting]
        public LoopType loopType = LoopType.Restart;
        public float startInterval = 0;
        public float endInterval = 0;

        public bool HasLoop => this.loops != 0;
    }
}
