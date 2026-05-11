using com.github.zehsteam.LocalMultiplayer.Managers;
using HarmonyLib;

namespace com.github.zehsteam.LocalMultiplayer.Patches;

[HarmonyPatch(typeof(NetworkManager))]
internal static class NetworkManager_Patches
{
    [HarmonyPatch(nameof(NetworkManager.OnDisconnected))]
    [HarmonyPostfix]
    private static void OnDisconnectedPatch()
    {
        SteamAccountManager.UnassignSpoofAccount();
    }
}
