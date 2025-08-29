using HarmonyLib;

namespace com.github.zehsteam.LocalMultiplayer.Patches
{
    [HarmonyPatch(typeof(SemiFunc))]
    internal static class SemiFuncPatch
    {
        [HarmonyPatch(nameof(SemiFunc.OnSceneSwitch))]
        [HarmonyPostfix]
        private static void OnSceneSwitchPatch(bool _leaveGame)
        {
            if (_leaveGame)
            {
                SteamAccountManager.UnassignSpoofAccount();
            }
        }
    }
}
