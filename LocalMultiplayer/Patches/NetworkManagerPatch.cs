using com.empress.LocalMultiplayer.Managers;
using HarmonyLib;

namespace com.empress.LocalMultiplayer.Patches;

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
