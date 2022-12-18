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
    }
}
