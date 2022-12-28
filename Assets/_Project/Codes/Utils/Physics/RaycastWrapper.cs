using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

using JoaoSant0s.Extensions.GameObjects;

namespace VTTRPG.WrapperPhysics
{
    public static class RaycastWrapper
    {
        public static bool RaycastUIFirst<T>(PointerEventData eventData, out T hitResult)
        {
            List<RaycastResult> objectsHit = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, objectsHit);

            RaycastResult result = objectsHit.Find(local => local.gameObject.HasComponent<T>());

            hitResult = default(T);
            if (result.gameObject == null) return false;

            hitResult = result.gameObject.GetComponent<T>();
            return true;
        }

        public static bool RaycastUIFirst<T>(Vector2 point, out T hitResult)
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = point;

            var result = RaycastUIFirst<T>(eventData, out T internalResult);
            hitResult = internalResult;
            
            return result;
        }
    }
}
