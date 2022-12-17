using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using VTTRPG.CustomServices;

using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Canvases;

namespace VTTRPG.Views.Attachment
{
    [RequireComponent(typeof(RectTransform))]
    public class ConfiningViewAttachment : MonoBehaviour
    {
        [CanvasIdAttribute]
        [SerializeField]
        private string canvasId;

        [SerializeField]
        private bool centralizePointWhenAwake;
        private DefaultInputService defaultInputService;

        #region Unity Methods

        private void Awake()
        {
            this.defaultInputService = Services.Get<DefaultInputService>();

            if (!centralizePointWhenAwake) return;

            CentralizeByInputPoint();
        }

        #endregion

        #region Private Methods

        private void CentralizeByInputPoint()
        {
            if (!this.defaultInputService.UIPoint(canvasId, out Vector2 point)) return;
            ((RectTransform)transform).localPosition = point;
        }

        #endregion

    }
}
