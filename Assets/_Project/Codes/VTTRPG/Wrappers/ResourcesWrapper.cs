using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using VTTRPG.Data.Assets;
using VTTRPG.Configs;

namespace VTTRPG.Assets
{
    public static class ResourcesWrapper
    {
        private static SystemTypeAsset[] systemAssets;

        public static SystemViewAsset LoadSystemViewAsset(string systemId)
        {
            return Resources.Load<SystemViewAsset>($"VTTRPG/Systems/{systemId}/SystemViewAsset");
        }

        public static PropertyAsset[] LoadProperties(string systemId, string propertiesPath)
        {
            return Resources.LoadAll<PropertyAsset>($"VTTRPG/Systems/{systemId}/{propertiesPath}");
        }

        public static T LoadSystemConfig<T>(string systemId) where T : SystemConfig
        {
            return Resources.Load<T>($"VTTRPG/Systems/{systemId}/Config");
        }

        public static GeneralConfig LoadGeneralConfig()
        {
            return Resources.Load<GeneralConfig>($"VTTRPG/Systems/Config");
        }

        public static SystemTypeAsset[] LoadSystemAssets()
        {
            if (systemAssets == null) systemAssets = Resources.LoadAll<SystemTypeAsset>("VTTRPG/SheetTypes");
            return systemAssets;
        }

        public static void UnloadSystemAssets()
        {
            if (systemAssets == null) return;

            foreach (var asset in systemAssets)
            {
                Resources.UnloadAsset(asset);
            }

            systemAssets = null;
        }
    }
}
