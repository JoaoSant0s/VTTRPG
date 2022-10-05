using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using VTTRPG.RPGSystems.Assets;

namespace VTTRPG.Assets
{
    public static class ResourcesWrapper
    {
        private static SystemTypeAsset[] systemAssets;

        public static SystemTypeAsset[] LoadSystemAssets()
        {
            if (systemAssets == null) systemAssets = Resources.LoadAll<SystemTypeAsset>("/");
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
