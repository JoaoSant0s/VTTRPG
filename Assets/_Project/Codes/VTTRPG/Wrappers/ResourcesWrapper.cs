using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using VTTRPG.Assets;

namespace VTTRPG.Wrappers
{
    public static class ResourcesWrapper
    {
        public static RPGViewAsset LoadRPGViewAsset(string rpgId)
        {
            return Resources.Load<RPGViewAsset>($"VTTRPG/RPGs/{rpgId}/RPGViewAsset");
        }
    }
}
