using com.github.zehsteam.LocalMultiplayer.Helpers;
using com.github.zehsteam.LocalMultiplayer.Managers;
using HarmonyLib;
using Steamworks;
using Steamworks.Data;

namespace com.github.zehsteam.LocalMultiplayer.Patches;

[HarmonyPatch(typeof(MenuPageMain))]
internal static class MenuPageMain_Patches
{
    [HarmonyPatch(nameof(MenuPageMain.ButtonEventJoinGame))]
    [HarmonyPrefix]
    private static bool ButtonEventJoinGame_Patch()
    {
        SteamAccountManager.AssignSpoofAccount();

        SteamManager.instance?.OnGameLobbyJoinRequested(new Lobby(GlobalSaveHelper.SteamLobbyId.Value), SteamClient.SteamId);

        return false;
    }
}
