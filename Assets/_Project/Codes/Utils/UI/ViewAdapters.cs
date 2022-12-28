using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace VTTRPG.UIWrappers
{
    public static class ViewAdapters
    {
        public static string ApplySignal(int value)
        {
            if (value >= 0) return $"+{value}";
            return $"{value}";
        }
    }
}
