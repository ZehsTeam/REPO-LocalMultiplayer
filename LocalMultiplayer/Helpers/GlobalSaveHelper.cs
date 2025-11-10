using com.github.zehsteam.LocalMultiplayer.Objects;
using System.Collections.Generic;

namespace com.github.zehsteam.LocalMultiplayer.Helpers;

internal static class GlobalSaveHelper
{
    public static JsonSave JsonSave => Plugin.GlobalSave;

    public static JsonSaveValue<ulong> SteamLobbyId { get; private set; }
    public static JsonSaveValue<List<SteamAccount>> SpoofSteamAccounts { get; private set; }
    public static JsonSaveValue<List<SteamAccount>> SpoofSteamAccountsInUse { get; private set; }

    static GlobalSaveHelper()
    {
        SteamLobbyId =            new JsonSaveValue<ulong>(JsonSave,              key: "SteamLobbyId");
        SpoofSteamAccounts =      new JsonSaveValue<List<SteamAccount>>(JsonSave, key: "SpoofSteamAccounts",      defaultValue: []);
        SpoofSteamAccountsInUse = new JsonSaveValue<List<SteamAccount>>(JsonSave, key: "SpoofSteamAccountsInUse", defaultValue: []);
    }
}
