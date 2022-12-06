using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace VTTRPG.Wrappers
{
    public static class RPGMathfs
    {
        public static int CalculateModifier(int value, int factor = 10)
        {
            if (value < factor) value -= 1;
            return (value - factor) / 2;
        }
    }
}
