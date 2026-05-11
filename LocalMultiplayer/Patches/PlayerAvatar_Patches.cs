using HarmonyLib;
using Photon.Pun;
using Steamworks;

namespace com.github.zehsteam.LocalMultiplayer.Patches;

[HarmonyPatch(typeof(PlayerAvatar))]
internal static class PlayerAvatar_Patches
{
    [HarmonyPatch(nameof(PlayerAvatar.AddToStatsManager))]
    [HarmonyPrefix]
    private static void AddToStatsManager_Patch()
    {
        PhotonNetwork.NickName = SteamClient.Name;
    }
}
