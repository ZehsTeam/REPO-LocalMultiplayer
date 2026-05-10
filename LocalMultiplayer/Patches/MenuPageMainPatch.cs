using com.empress.LocalMultiplayer.Helpers;
using com.empress.LocalMultiplayer.Managers;
using HarmonyLib;
using Steamworks;
using Steamworks.Data;

namespace com.empress.LocalMultiplayer.Patches;

[HarmonyPatch(typeof(MenuPageMain))]
internal static class MenuPageMainPatch
{
    [HarmonyPatch(nameof(MenuPageMain.ButtonEventJoinGame))]
    [HarmonyPrefix]
    private static bool ButtonEventJoinGamePatch()
    {
        SteamAccountManager.AssignSpoofAccount();

        SteamManager.instance?.OnGameLobbyJoinRequested(new Lobby(GlobalSaveHelper.SteamLobbyId.Value), SteamClient.SteamId);

        return false;
    }
}
