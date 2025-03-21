﻿using BepInEx;
using BepInEx.Configuration;
using System.IO;
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
        BepInPlugin metadata = MetadataHelper.GetMetadata(plugin);
        return CreateConfigFile(plugin, Paths.ConfigPath, name, saveOnInit);
    }

    public static ConfigFile CreateGlobalConfigFile(BaseUnityPlugin plugin, string name = null, bool saveOnInit = false)
    {
        BepInPlugin metadata = MetadataHelper.GetMetadata(plugin);
        string path = Path.Combine(Application.persistentDataPath, metadata.Name);
        name ??= "global";
        return CreateConfigFile(plugin, path, name, saveOnInit);
    }
}
