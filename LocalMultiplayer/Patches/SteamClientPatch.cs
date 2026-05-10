using com.empress.LocalMultiplayer.Managers;
using HarmonyLib;
using Steamworks;

namespace com.empress.LocalMultiplayer.Patches;

[HarmonyPatch(typeof(SteamClient))]
internal static class SteamClientPatch
{
    [HarmonyPatch(nameof(SteamClient.Name), MethodType.Getter)]
    [HarmonyPrefix]
    private static bool NamePatch(ref string __result)
    {
        if (!SteamAccountManager.IsUsingSpoofAccount)
        {
            return true;
        }

        __result = SteamAccountManager.SpoofAccount.Username;
        return false;
    }

    [HarmonyPatch(nameof(SteamClient.SteamId), MethodType.Getter)]
    [HarmonyPrefix]
    private static bool SteamIdPatch(ref SteamId __result)
    {
        if (!SteamAccountManager.IsUsingSpoofAccount)
        {
            return true;
        }

        __result = SteamAccountManager.SpoofAccount.SteamId;
        return false;
    }
}
