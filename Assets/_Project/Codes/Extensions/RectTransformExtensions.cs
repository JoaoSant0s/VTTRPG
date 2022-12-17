using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace VTTRPG.Extensions
{
    public static class RectTransformExtensions
    {
        public static void ModifyAnchoredPositionY(this RectTransform transform, float yAxis)
        {
            transform.anchoredPosition += new Vector2(0, yAxis);
        }

        public static void ModifyAnchoredPositionX(this RectTransform transform, float xAxis)
        {
            transform.anchoredPosition += new Vector2(xAxis, 0);
        }
    }
}
