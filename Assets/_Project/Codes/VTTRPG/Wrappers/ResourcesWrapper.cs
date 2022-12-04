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

        public static PropertyAsset[] LoadProperties(string rpgId, string propertiesPath)
        {
            return Resources.LoadAll<PropertyAsset>($"VTTRPG/RPGs/{rpgId}/{propertiesPath}");
        }

        public static T LoadRPGConfig<T>(string rpgId) where T : RPGConfig
        {
            return Resources.Load<T>($"VTTRPG/RPGs/{rpgId}/RPGConfig");
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
