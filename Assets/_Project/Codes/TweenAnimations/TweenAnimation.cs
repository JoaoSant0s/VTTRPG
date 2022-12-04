using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DG.Tweening;

namespace Common.TweenAnimations
{
    public class TweenAnimation : MonoBehaviour
    {
        [Header("Control")]
        public bool autoRun = true;

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

        #region Public Abstract Methods


        #endregion

        #region Public Virtual Methods

        public virtual void Run(Action startCallback = null, Action endCallback = null)
        {

        }

        public virtual void CompleteTween()
        {
            if (this.sequence != null)
            {
                this.sequence.Complete();
                this.sequence.Kill();
            }
        }

        #endregion
    }
}
