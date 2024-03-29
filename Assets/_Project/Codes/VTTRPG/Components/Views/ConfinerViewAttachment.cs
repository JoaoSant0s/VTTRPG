using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NaughtyAttributes;

using VTTRPG.CustomServices;

using JoaoSant0s.ServicePackage.General;
using JoaoSant0s.ServicePackage.Canvases;
using JoaoSant0s.Extensions.RectTransforms;

namespace VTTRPG.Views.Attachment
{
    [RequireComponent(typeof(RectTransform))]
    public class ConfinerViewAttachment : MonoBehaviour
    {
        private enum HorizontalConfinerType
        {
            Left,
            Right,
            Adaptive
        }

        private enum VerticalConfinerType
        {
            Top,
            Bottom,
            Adaptive
        }

        [Header("References")]
        [CanvasIdAttribute]
        [SerializeField]
        private string canvasId;

        [Header("Configs")]
        [SerializeField]
        private bool centralizePointWhenAwake;

        [BoxGroup("Vertical")]
        [SerializeField]
        private VerticalConfinerType verticalTypeWhenAwake;
        [BoxGroup("Vertical")]
        [SerializeField]
        private float verticalOffset;

        [BoxGroup("Horizontal")]

        [SerializeField]
        private HorizontalConfinerType horizontalTypeWhenAwake;
        [BoxGroup("Horizontal")]
        [SerializeField]
        private float horizontalOffset;

        private InputService inputService;

        private RectTransform rectTransform;

        private Vector2 ScreenLimits => new Vector2(Screen.width / 2, Screen.height / 2);
        private Vector2 RectLimits => new Vector2(this.rectTransform.rect.width / 2, this.rectTransform.rect.height / 2);

        #region Unity Methods

        private void Awake()
        {
            this.inputService = Services.Get<InputService>();
            this.rectTransform = ((RectTransform)transform);

            if (!this.centralizePointWhenAwake) return;

            CentralizeByInputPoint();
            ConfinerView();
        }

        #endregion

        #region Private Methods

        private void CentralizeByInputPoint()
        {
            if (!this.inputService.UIPoint(canvasId, out Vector2 point)) return;
            this.rectTransform.anchoredPosition = point;
        }

        private void ConfinerView()
        {
            ModifyVerticalConfiner();
            ModifyHorizontalConfiner();
        }

        private void ModifyVerticalConfiner()
        {
            switch (verticalTypeWhenAwake)
            {
                case VerticalConfinerType.Top:
                    TryChangeVerticalToTop();
                    break;
                case VerticalConfinerType.Bottom:
                    TryChangeVerticalToBottom();
                    break;
                case VerticalConfinerType.Adaptive:
                    TryChangeVerticalToBottom();
                    TryChangeVerticalToTop();
                    break;
            }
        }

        private void TryChangeVerticalToTop()
        {
            var newYAxis = this.rectTransform.anchoredPosition.y + RectLimits.y;
            if (newYAxis > ScreenLimits.y) this.rectTransform.IncrementAnchoredPositionY(-(newYAxis + verticalOffset - ScreenLimits.y));
        }

        private void TryChangeVerticalToBottom()
        {
            var newYAxis = this.rectTransform.anchoredPosition.y - RectLimits.y;
            if (newYAxis < -ScreenLimits.y) this.rectTransform.IncrementAnchoredPositionY(Mathf.Abs(newYAxis + ScreenLimits.y) + verticalOffset);
        }

        private void ModifyHorizontalConfiner()
        {
            var newXAxis = this.rectTransform.anchoredPosition.x + RectLimits.x;

            switch (horizontalTypeWhenAwake)
            {
                case HorizontalConfinerType.Left:
                    TryChangeHorizontalToLeft();
                    break;
                case HorizontalConfinerType.Right:
                    TryChangeHorizontalToRight();
                    break;
                case HorizontalConfinerType.Adaptive:
                    TryChangeHorizontalToRight();
                    TryChangeHorizontalToLeft();
                    break;
            }
        }

        private void TryChangeHorizontalToLeft()
        {
            var newXAxis = this.rectTransform.anchoredPosition.x - RectLimits.x;
            if (newXAxis < -ScreenLimits.x) this.rectTransform.IncrementAnchoredPositionX(Mathf.Abs(newXAxis + ScreenLimits.x) + horizontalOffset);
        }

        private void TryChangeHorizontalToRight()
        {
            var newXAxis = this.rectTransform.anchoredPosition.x + RectLimits.x;
            if (newXAxis > ScreenLimits.x) this.rectTransform.IncrementAnchoredPositionX(-(newXAxis + horizontalOffset - ScreenLimits.x));
        }

        #endregion

    }
}
