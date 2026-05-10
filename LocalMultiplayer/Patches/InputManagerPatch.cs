using com.empress.LocalMultiplayer.Managers;
using HarmonyLib;

namespace com.empress.LocalMultiplayer.Patches;

[HarmonyPatch(typeof(InputManager))]
internal static class InputManagerPatch
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
