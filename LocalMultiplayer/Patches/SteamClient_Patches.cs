using com.github.zehsteam.LocalMultiplayer.Managers;
using HarmonyLib;
using Steamworks;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace com.github.zehsteam.LocalMultiplayer.Patches;

[HarmonyPatch(typeof(SteamClient))]
internal static class SteamClient_Patches
{
    [HarmonyPatch(nameof(SteamClient.Name), MethodType.Getter)]
    [HarmonyPrefix]
    private static bool Name_Patch(ref string __result)
    {
        if (!SteamAccountManager.IsUsingSpoofAccount)
        {
            return true;
        }

        __result = SteamAccountManager.SpoofAccount.Username;
        return false;
    }

    [HarmonyPatch(nameof(SteamClient.SteamId), MethodType.Getter)]
    [HarmonyPrefix]
    private static bool SteamId_Patch(ref SteamId __result)
    {
        if (!SteamAccountManager.IsUsingSpoofAccount)
        {
            Logger.LogWarning($"\n\nGetting Steam ID (REAL)\n");

            LogStackTraceSignatures();

            return true;
        }

        __result = SteamAccountManager.SpoofAccount.SteamId;

        Logger.LogWarning($"\n\nGetting Steam ID (SPOOFED: {__result})\n");

        LogStackTraceSignatures();

        return false;
    }

    public static void LogStackTraceSignatures()
    {
        var stackTrace = new StackTrace(skipFrames: 1, fNeedFileInfo: false);
        var sb = new StringBuilder();

        int index = 1;
        foreach (var frame in stackTrace.GetFrames() ?? Array.Empty<StackFrame>())
        {
            MethodBase method = frame.GetMethod();
            if (method == null) continue;

            string declaringType = method.DeclaringType?.FullName ?? "<unknown>";
            string parameters = string.Join(", ",
                Array.ConvertAll(method.GetParameters(),
                    p => $"{p.ParameterType.Name} {p.Name}")
            );

            sb.AppendLine($"{index,2}. {declaringType}.{method.Name}({parameters})");
            index++;
        }

        Logger.LogInfo($"{nameof(LogStackTraceSignatures)}:\n{sb.ToString()}", extended: true);
    }
}
