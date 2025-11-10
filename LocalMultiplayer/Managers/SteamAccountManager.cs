using com.github.zehsteam.LocalMultiplayer.Helpers;
using com.github.zehsteam.LocalMultiplayer.Objects;
using Photon.Pun;
using Steamworks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace com.github.zehsteam.LocalMultiplayer.Managers;

internal static class SteamAccountManager
{
    public static SteamAccount RealAccount { get; private set; }

    public static SteamAccount SpoofAccount;

    public static bool IsUsingSpoofAccount => SpoofAccount != default;

    private static bool _initialized;

    public static void Initialize()
    {
        if (_initialized)
        {
            return;
        }

        RealAccount = new SteamAccount(SteamClient.Name, SteamClient.SteamId);

        CreateSpoofAccounts();

        Application.quitting += UnassignSpoofAccount;

        _initialized = true;
    }

    private static void CreateSpoofAccounts()
    {
        List<SteamAccount> accounts = GlobalSaveHelper.SpoofSteamAccounts.Value;

        int amountToAdd = 5;

        if (accounts.Count >= amountToAdd)
        {
            return;
        }

        for (int i = accounts.Count; i < amountToAdd; i++)
        {
            accounts.Add(new SteamAccount($"Player {i + 2}", SteamHelper.GenerateRandomSteamId()));
        }

        GlobalSaveHelper.SpoofSteamAccounts.Value = accounts;
    }

    public static void AssignSpoofAccount()
    {
        Logger.LogInfo($"SteamAccountManager: AssignSpoofAccount(); IsUsingSpoofAccount: {IsUsingSpoofAccount}");

        if (IsUsingSpoofAccount)
        {
            return;
        }

        SteamAccount account = GetAvailableSpoofAccount();

        AddSpoofAccountInUse(account);

        SpoofAccount = account;

        PhotonNetwork.NickName = account.Username;

        Logger.LogInfo($"SteamAccountManager: Set spoof account (Username: \"{account.Username}\", SteamID: {account.SteamId})");
    }

    public static void UnassignSpoofAccount()
    {
        Logger.LogInfo($"SteamAccountManager: UnassignSpoofAccount(); IsUsingSpoofAccount: {IsUsingSpoofAccount}");

        if (!IsUsingSpoofAccount)
        {
            return;
        }

        RemoveSpoofAccountInUse(SpoofAccount);

        SpoofAccount = default;

        PhotonNetwork.NickName = RealAccount.Username;

        Logger.LogInfo($"SteamAccountManager: UnassignSpoofAccount(); Unassigned spoof account.");
    }

    public static void ResetSpoofAccountsInUse()
    {
        GlobalSaveHelper.SpoofSteamAccountsInUse.Value = [];
    }

    public static bool TryGetSpoofAccount(ulong steamId, out SteamAccount steamAccount)
    {
        var accounts = GlobalSaveHelper.SpoofSteamAccounts.Value;

        foreach (var account in accounts)
        {
            if (account.SteamId == steamId)
            {
                steamAccount = account;
                return true;
            }
        }

        steamAccount = default;
        return false;
    }

    public static void SetCurrentSpoofAccountColor(int id)
    {
        if (!IsUsingSpoofAccount)
        {
            return;
        }

        SpoofAccount.ColorId = id;

        UpdateCurrentSpoofAccountData();
    }

    private static void UpdateCurrentSpoofAccountData()
    {
        if (!IsUsingSpoofAccount)
        {
            return;
        }

        var accounts = GlobalSaveHelper.SpoofSteamAccounts.Value;

        for (int i = 0; i < accounts.Count; i++)
        {
            if (accounts[i] == SpoofAccount)
            {
                accounts[i] = SpoofAccount;
                break;
            }
        }

        GlobalSaveHelper.SpoofSteamAccounts.Value = accounts;
    }

    private static List<SteamAccount> GetAvailableSpoofAccounts()
    {
        List<SteamAccount> availableAccounts = [];

        var accountsInUse = GlobalSaveHelper.SpoofSteamAccountsInUse.Value;

        foreach (var account in GlobalSaveHelper.SpoofSteamAccounts.Value)
        {
            if (accountsInUse.Contains(account))
            {
                continue;
            }

            availableAccounts.Add(account);
        }

        return availableAccounts;
    }

    private static SteamAccount GetAvailableSpoofAccount()
    {
        var accounts = GetAvailableSpoofAccounts();

        if (accounts.Count == 0)
        {
            Logger.LogWarning("SteamAccountManager: No cached spoof steam accounts available. Generating new spoof steam account.");
            return new SteamAccount($"Player {Random.Range(100, 999)}", SteamHelper.GenerateRandomSteamId());
        }

        return accounts[0];
    }

    private static void AddSpoofAccountInUse(SteamAccount accountToAdd)
    {
        var accounts = GlobalSaveHelper.SpoofSteamAccountsInUse.Value;

        if (accounts.Contains(accountToAdd))
        {
            return;
        }

        accounts.Add(accountToAdd);

        GlobalSaveHelper.SpoofSteamAccountsInUse.Value = accounts;
    }

    private static void RemoveSpoofAccountInUse(SteamAccount accountToRemove)
    {
        List<SteamAccount> accountsInUse = GlobalSaveHelper.SpoofSteamAccountsInUse.Value;
        GlobalSaveHelper.SpoofSteamAccountsInUse.Value = accountsInUse.Where(x => x.SteamId != accountToRemove.SteamId).ToList();
    }
}
