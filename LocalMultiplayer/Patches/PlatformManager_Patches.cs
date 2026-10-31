using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using Platforms;

namespace com.github.zehsteam.LocalMultiplayer.Patches;

[HarmonyPatch(typeof(PlatformManager))]
internal static class PlatformManager_Patches
{
    [HarmonyPatch(nameof(PlatformManager.SetPhotonAuthTicket))]
    [HarmonyPostfix]
    private static void SetPhotonAuthTicket_Patch()
    {
        if (PlatformManager.instance.activePlatform.IsInitialised())
        {
            // Remove Steam auth
            PhotonNetwork.AuthValues.AuthType = CustomAuthenticationType.None;
        }
    }
}
