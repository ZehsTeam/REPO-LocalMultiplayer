using Steamworks;

namespace com.github.zehsteam.LocalMultiplayer.Helpers;

internal static class SteamHelper
{
    public static ulong GenerateRandomSteamId()
    {
        // SteamID64 base value for individual users
        ulong steamIdBase = 76561197960265728;
        
        // Generate a random 32-bit unsigned integer
        var random = new System.Random();
        uint randomPart = (uint)random.Next(0, int.MaxValue);
        randomPart += (uint)random.Next(0, int.MaxValue);

        return steamIdBase + randomPart;
    }

    public static bool IsValidClient()
    {
        if (SteamClient.Name == "IGGGAMES")
            return false;

        if (SteamClient.SteamId == 12345678)
            return false;

        if (SteamClient.AppId == 480)
            return false;

        AuthTicket authTicket = SteamManager.instance.steamAuthTicket;
        if (authTicket == null) return false;

        BeginAuthResult beginAuthResult = SteamUser.BeginAuthSession(authTicket.Data, SteamClient.SteamId);
        return beginAuthResult == BeginAuthResult.OK;
    }
}
