using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using VTTRPG.Assets;

namespace VTTRPG.Wrappers
{
    public static class ResourcesWrapper
    {
        private static RPGTypeAsset[] rpgAssets;

        public static RPGViewAsset LoadRPGViewAsset(string rpgId)
        {
            return Resources.Load<RPGViewAsset>($"VTTRPG/RPGs/{rpgId}/RPGViewAsset");
        }

        public static GeneralConfig LoadGeneralConfig()
        {
            return Resources.Load<GeneralConfig>($"VTTRPG/GeneralConfig");
        }

        public static RPGTypeAsset[] LoadRPGAssets()
        {
            if (rpgAssets == null) rpgAssets = Resources.LoadAll<RPGTypeAsset>("VTTRPG/RPGTypes");
            return rpgAssets;
        }
    }
}
