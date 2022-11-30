using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace VTTRPG.Wrappers
{
    public static class UtilWrappers
    {
        public static string ApplySignal(int value)
        {
            if (value >= 0) return $"+{value}";
            return $"{value}";
        }
    }
}
