using com.github.zehsteam.LocalMultiplayer.Helpers;
using com.github.zehsteam.LocalMultiplayer.Managers;
using HarmonyLib;
using Steamworks;
using Steamworks.Data;
using UnityEngine;

namespace com.github.zehsteam.LocalMultiplayer.Patches;

[HarmonyPatch(typeof(SteamLobbyManager))]
internal static class SteamLobbyManager_Patches
{
    [HarmonyPatch(nameof(SteamLobbyManager.Awake))]
    [HarmonyPostfix]
    [HarmonyPriority(Priority.First)]
    private static void Awake_Patch()
    {
        SteamAccountManager.Initialize();
    }

    [HarmonyPatch(nameof(SteamLobbyManager.Start))]
    [HarmonyPostfix]
    private static void Start_Patch()
    {
        if (!SteamHelper.IsValidClient())
            Application.Quit();
    }

    [HarmonyPatch(nameof(SteamLobbyManager.OnLobbyCreated))]
    [HarmonyPostfix]
    private static void OnLobbyCreated_Patch(ref Result _result, ref Lobby _lobby)
    {
        if (_result != Result.OK)
            return;

        GlobalSaveHelper.SteamLobbyId.Value = _lobby.Id;

        SteamAccountManager.ResetSpoofAccountsInUse();
    }
}
