using BepInEx;
using BepInEx.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEngine;

namespace com.github.zehsteam.LocalMultiplayer;

internal static class Utils
{
    public static string GetPluginPersistentDataPath()
    {
        return Path.Combine(Application.persistentDataPath, MyPluginInfo.PLUGIN_NAME);
    }

    public static ConfigFile CreateConfigFile(BaseUnityPlugin plugin, string path, string name = null, bool saveOnInit = false)
    {
        BepInPlugin metadata = MetadataHelper.GetMetadata(plugin);
        name ??= metadata.GUID;
        name += ".cfg";
        return new ConfigFile(Path.Combine(path, name), saveOnInit, metadata);
    }

    public static ConfigFile CreateLocalConfigFile(BaseUnityPlugin plugin, string name = null, bool saveOnInit = false)
    {
        return CreateConfigFile(plugin, Paths.ConfigPath, name, saveOnInit);
    }

    public static ConfigFile CreateGlobalConfigFile(BaseUnityPlugin plugin, string name = null, bool saveOnInit = false)
    {
        string path = GetPluginPersistentDataPath();
        name ??= "global";
        return CreateConfigFile(plugin, path, name, saveOnInit);
    }

    /// <summary>
    /// Returns a string representation of the current stack trace.
    /// </summary>
    /// <param name="skipFrames">Number of initial stack frames to skip (default is 0).</param>
    /// <param name="includeFileInfo">Whether to include file names and line numbers (default is true).</param>
    /// <returns>A string containing the current stack trace.</returns>
    public static string GetCurrentStackTrace(int skipFrames = 0, bool includeFileInfo = true)
    {
        // Skip this method and optionally more frames if specified
        var stackTrace = new StackTrace(skipFrames + 1, includeFileInfo);
        return stackTrace.ToString();
    }

    /// <summary>
    /// Returns a formatted list of method calls leading to this point.
    /// </summary>
    /// <param name="skipFrames">Number of initial frames to skip (default is 1 to skip this method itself).</param>
    /// <returns>A string showing the ordered list of calling functions.</returns>
    public static string GetCallStackMethods(int skipFrames = 1)
    {
        var stackTrace = new StackTrace(skipFrames, false);
        var frames = stackTrace.GetFrames();
        if (frames == null || frames.Length == 0)
            return "No stack trace available.";

        var sb = new StringBuilder();
        int index = 1;

        foreach (var frame in frames)
        {
            var method = frame.GetMethod();
            if (method == null) continue;

            string methodName = method.Name;
            string declaringType = method.DeclaringType != null
                ? method.DeclaringType.FullName
                : "<UnknownType>";

            sb.AppendLine($"{index}: {declaringType}.{methodName}()");
            index++;
        }

        return sb.ToString();
    }
}
