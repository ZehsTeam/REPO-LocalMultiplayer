using com.github.zehsteam.LocalMultiplayer.Managers;
using HarmonyLib;

namespace com.github.zehsteam.LocalMultiplayer.Patches;

[HarmonyPatch(typeof(MetaManager))]
internal static class MetaManager_Patches
{
    [HarmonyPatch(nameof(MetaManager.Save))]
    [HarmonyPrefix]
    private static bool Save_Patch()
    {
        if (!SteamAccountManager.IsUsingSpoofAccount)
        {
            return true;
        }

        return false;
    }
}
