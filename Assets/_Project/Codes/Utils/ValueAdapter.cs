using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using JoaoSant0s.Extensions.Colors;

using ObjectValues.CoreValues;
using VTTRPG.ColorWrappers;

namespace VTTRPG.ValueAdapters
{
    public static class ValueAdapter
    {
        public static ColorValue ToColorValue(StringValue stringValue)
        {
            if (stringValue == null || stringValue.value == null) return null;
            var values = stringValue.value.Split(":");
            var parsed = ColorWrapper.HexToColor(values[0], out Color color, float.Parse(values[1]));
            Debug.Assert(parsed, $"The hexadecimal value {values[0]} can be parsed to Color");
            return new ColorValue(stringValue.id, color);
        }

        public static StringValue ToStringValue(ColorValue colorValue)
        {
            var hexString = colorValue.value.ToHex();
            var alpha = colorValue.value.a;

            return new StringValue(colorValue.id, $"{hexString}:{alpha}");
        }
    }
}
