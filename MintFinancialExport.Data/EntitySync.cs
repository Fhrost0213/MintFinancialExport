﻿using MintFinancialExport.Core.Entities;
using MintFinancialExport.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MintFinancialExport.Data
{
    public static class EntitySync
    {
        public static void SyncAccounts(ObservableCollection<MintAccount> accountlist)
        {
            int? nextRunId = 0;
            var latestRun = DataAccess.GetList<AccountHistory>().OrderByDescending(a => a.RunId).FirstOrDefault();
            if (latestRun != null) nextRunId = latestRun.RunId + 1;
            DateTime asOfDate = System.DateTime.Now;

            SyncAccounts(accountlist, null, nextRunId, asOfDate);
        }

        public static void SyncAccounts(ObservableCollection<MintAccount> accountlist, List<AccountHistory> manualAccountHistory)
        {
            int? nextRunId = 0;
            var latestRun = DataAccess.GetList<AccountHistory>().OrderByDescending(a => a.RunId).FirstOrDefault();
            if (latestRun != null) nextRunId = latestRun.RunId + 1;
            DateTime asOfDate = System.DateTime.Now;

            SyncAccounts(accountlist, manualAccountHistory, nextRunId, asOfDate);
        }

        public static void SyncAccounts(ObservableCollection<MintAccount> accountlist, List<AccountHistory> manualAccountHistory, int? nextRunId, DateTime asOfDate)
        {
            // Sync accounts with DB
            foreach (var account in accountlist)
            {
                Account accountItem = DataAccess.GetList<Account>().FirstOrDefault(n => n.AccountName.Equals(account.Name));
                if (accountItem == null)
                {
                    accountItem = new Account();
                    accountItem.AccountName = account.Name;
                }

                //DataAccess.SaveItem(accountItem);

                // Store off a snapshot of account history
                AccountHistory accountHistory = new AccountHistory();
                accountHistory.Account = accountItem;
                accountHistory.AccountId = accountItem.ObjectId;
                accountHistory.Amount = account.Value;
                accountHistory.AsOfDate = asOfDate;
                accountHistory.RunId = nextRunId;

                DataAccess.SaveItem(accountHistory);
            }

            if (manualAccountHistory != null) DataAccess.SaveList(manualAccountHistory);

            SyncNetWorth(nextRunId);
        }

        public static void SyncNetWorth(int? runId)
        {
            decimal? assetsTotal = 0;
            decimal? debtsTotal = 0;

            ExportObjects objects = new ExportObjects();
            var exportAccountList = objects.GetExportAccountList();

            var assets = exportAccountList.Where(n => n.IsAsset == true && n.AccountTypeID != 99);
            var debts = exportAccountList.Where(n => n.IsAsset == false && n.AccountTypeID != 99);

            foreach (var asset in assets)
            {
                assetsTotal = assetsTotal + asset.Value;
            }

            foreach(var debt in debts)
            {
                debtsTotal = debtsTotal + debt.Value;
            }

            NetWorthHistory NetWorthHistory = new NetWorthHistory();
            NetWorthHistory.NetWorthAmount = assetsTotal + debtsTotal;
            NetWorthHistory.AssetAmount = assetsTotal;
            NetWorthHistory.DebtAmount = debtsTotal;
            NetWorthHistory.AsOfDate = DateTime.Now;
            NetWorthHistory.RunId = runId;

            DataAccess.SaveItem(NetWorthHistory);
        }
    }
}