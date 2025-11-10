using BepInEx;
using BepInEx.Configuration;
using com.github.zehsteam.LocalMultiplayer.Objects;
using com.github.zehsteam.LocalMultiplayer.Patches;
using HarmonyLib;

namespace com.github.zehsteam.LocalMultiplayer;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
internal class Plugin : BaseUnityPlugin
{
    private readonly Harmony _harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);

    public static Plugin Instance { get; private set; }

    public new ConfigFile Config { get; private set; }

    public static JsonSave GlobalSave { get; private set; }

    private void Awake()
    {
        Instance = this;

        LocalMultiplayer.Logger.Initialize(BepInEx.Logging.Logger.CreateLogSource(MyPluginInfo.PLUGIN_GUID));
        LocalMultiplayer.Logger.LogInfo($"{MyPluginInfo.PLUGIN_NAME} has awoken!");

        Config = Utils.CreateGlobalConfigFile(this);

        GlobalSave = new JsonSave(Utils.GetPluginPersistentDataPath(), "GlobalSave");

        _harmony.PatchAll(typeof(SteamClientPatch));
        _harmony.PatchAll(typeof(DataDirectorPatch));
        _harmony.PatchAll(typeof(NetworkManagerPatch));
        _harmony.PatchAll(typeof(SteamManagerPatch));
        _harmony.PatchAll(typeof(InputManagerPatch));
        _harmony.PatchAll(typeof(MenuPageMainPatch));
        _harmony.PatchAll(typeof(PlayerAvatarPatch));

        ConfigManager.Initialize(Config);
    }
}
