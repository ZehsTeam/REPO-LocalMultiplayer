using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using Platforms;
using System;

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
            PhotonNetwork.AuthValues = new AuthenticationValues(Guid.NewGuid().ToString());
        }
    }
}
