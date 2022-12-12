using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace VTTRPG.Extensions
{
    public static class GameObjectExtensions
    {
        public static bool HasComponent<T>(this GameObject gameObject)
        {
            return gameObject.GetComponent<T>() != null;
        }
    }
}
