using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

namespace Common.TweenAnimations
{
    public abstract class TweenJoinAnimation : Tween
    {
        #region Protected Abstract Methods

        protected abstract List<Tweener> BuildJoinAnimations();

        #endregion

        #region Public Methods        

        public sealed override void Run(Action startCallback = null, Action endCallback = null)
        {
            CompleteTween();

            this.sequence = DOTween.Sequence();

            if (startCallback != null) this.sequence.PrependCallback(() => startCallback?.Invoke());
            if (sequenceParameters.startInterval > 0) this.sequence.PrependInterval(sequenceParameters.startInterval);

            var joins = BuildJoinAnimations();
            foreach (var item in joins) this.sequence.Join(item);

            if (sequenceParameters.endInterval > 0) this.sequence.AppendInterval(sequenceParameters.endInterval);
            if (endCallback != null) this.sequence.AppendCallback(() => endCallback?.Invoke());

            if (sequenceParameters.HasLoop) this.sequence.SetLoops(sequenceParameters.loops, sequenceParameters.loopType);

            this.sequence.Play();
        }

        #endregion
    }
}
