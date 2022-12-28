using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace VTTRPG.ColorWrappers
{
    public class ColorWrapper : MonoBehaviour
    {
        public static bool HexToColor(string hexValue, out Color color, float alpha = 1)
        {
            var converted = ColorUtility.TryParseHtmlString(hexValue, out Color internalColor);

            color = internalColor;
            color.a = alpha;

            return converted;
        }

        public static int PercentageToNumerical(float percange)
        {
            return (int)Mathf.Lerp(0, 255, Mathf.Clamp01(percange));
        }

        public static float NumericalToPercentage(int numerical)
        {
            return (float) (Mathf.Clamp(numerical, 0, 255) / 255f);
        }
    }
}
