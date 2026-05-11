using com.github.zehsteam.LocalMultiplayer.Managers;
using HarmonyLib;

namespace com.github.zehsteam.LocalMultiplayer.Patches;

[HarmonyPatch(typeof(InputManager))]
internal static class InputManager_Patches
{
    [HarmonyPatch(nameof(InputManager.SaveDefaultKeyBindings))]
    [HarmonyPrefix]
    private static bool SaveDefaultKeyBindingsPatch()
    {
        return !SteamAccountManager.IsUsingSpoofAccount;
    }

    [HarmonyPatch(nameof(InputManager.SaveCurrentKeyBindings))]
    [HarmonyPrefix]
    private static bool SaveCurrentKeyBindingsPatch()
    {
        return !SteamAccountManager.IsUsingSpoofAccount;
    }
}
