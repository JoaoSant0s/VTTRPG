using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using VTTRPG.Data.Assets;

namespace VTTRPG.Assets
{
    public static class ResourcesWrapper
    {
        private static SystemTypeAsset[] systemAssets;

        public static SystemViewAsset LoadSystemViewAsset(string systemId)
        {
            return Resources.Load<SystemViewAsset>($"VTTRPG/Systems/{systemId}/SystemViewAsset");
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
