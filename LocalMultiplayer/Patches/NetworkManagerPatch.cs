using com.github.zehsteam.LocalMultiplayer.Managers;
using HarmonyLib;

namespace com.github.zehsteam.LocalMultiplayer.Patches;

[HarmonyPatch(typeof(NetworkManager))]
internal static class NetworkManagerPatch
{
    [HarmonyPatch(nameof(NetworkManager.OnDisconnected))]
    [HarmonyPostfix]
    private static void OnDisconnectedPatch()
    {
        SteamAccountManager.UnassignSpoofAccount();
    }
}
