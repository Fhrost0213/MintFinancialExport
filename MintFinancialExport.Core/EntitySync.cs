﻿using MintFinancialExport.Core.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MintFinancialExport.Core.Interfaces;

namespace MintFinancialExport.Core
{
    public class EntitySync : IEntitySync
    {
        private IDataAccess _dataAccess;

        public EntitySync()
        {
            _dataAccess = ServiceLocator.GetInstance<IDataAccess>();
        }

        public void SyncAccounts(ObservableCollection<MintAccount> accountlist)
        {
            int? nextRunId = 0;
            var latestRun = _dataAccess.GetList<AccountHistory>().OrderByDescending(a => a.RunId).FirstOrDefault();
            if (latestRun != null) nextRunId = latestRun.RunId + 1;
            DateTime asOfDate = System.DateTime.Now;

            SyncAccounts(accountlist, null, nextRunId, asOfDate);
        }

        public void SyncAccounts(ObservableCollection<MintAccount> accountlist, List<AccountHistory> manualAccountHistory)
        {
            int? nextRunId = 0;
            var latestRun = _dataAccess.GetList<AccountHistory>().OrderByDescending(a => a.RunId).FirstOrDefault();
            if (latestRun != null) nextRunId = latestRun.RunId + 1;
            DateTime asOfDate = System.DateTime.Now;

            SyncAccounts(accountlist, manualAccountHistory, nextRunId, asOfDate);
        }

        public void SyncAccounts(ObservableCollection<MintAccount> accountlist, List<AccountHistory> manualAccountList, int? nextRunId, DateTime asOfDate)
        {
            decimal? amount;

            // Sync accounts with DB
            foreach (var account in accountlist)
            {
                Account accountItem = _dataAccess.GetList<Account>().FirstOrDefault(n => n.AccountName.Equals(account.Name));
                if (accountItem == null)
                {
                    accountItem = new Account();
                    accountItem.AccountName = account.Name;
                }

                _dataAccess.SaveItem(accountItem);

                // Store off a snapshot of account history
                AccountHistory accountHistory = new AccountHistory();
                //accountHistory.Account = accountItem;
                accountHistory.AccountId = accountItem.ObjectId;
                accountHistory.Amount = account.Value;
                accountHistory.AsOfDate = asOfDate;
                accountHistory.RunId = nextRunId;

                _dataAccess.SaveItem(accountHistory);
            }

            // Process manual accounts
            foreach (var manualAccount in manualAccountList)
            {
                Account accountItem = _dataAccess.GetList<Account>().FirstOrDefault(n => n.ObjectId.Equals(manualAccount.AccountId));
                var mapping = accountItem.AccountMappings.FirstOrDefault();

                if (mapping.AccountTypeId == _dataAccess.GetList<AccountType>().First(n => n.AccountTypeName == "Physical").ObjectId)
                {
                    var preciousMetalsItem = _dataAccess.GetList<PreciousMetalsHistory>().OrderByDescending(d => d.AsOfDate).FirstOrDefault();
                    amount = (preciousMetalsItem.GoldOunces * preciousMetalsItem.GoldSpotPrice) + (preciousMetalsItem.SilverOunces * preciousMetalsItem.SilverSpotPrice)
                        + (preciousMetalsItem.PlatinumOunces * preciousMetalsItem.PlatinumSpotPrice) + (preciousMetalsItem.PalladiumOunces * preciousMetalsItem.PalladiumSpotPrice);
                }
                else
                {
                    amount = manualAccount.Amount;
                }

                // Store off a snapshot of account history
                AccountHistory accountHistory = new AccountHistory();
                //accountHistory.Account = accountItem;
                accountHistory.AccountId = accountItem.ObjectId;
                accountHistory.Amount = amount;
                accountHistory.AsOfDate = asOfDate;
                accountHistory.RunId = nextRunId;

                _dataAccess.SaveItem(accountHistory);
            }

            if (manualAccountList != null) _dataAccess.SaveList(manualAccountList);

            SyncNetWorth(nextRunId);
        }

        public void SyncNetWorth(int? runId)
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

            _dataAccess.SaveItem(NetWorthHistory);
        }

        public void RefreshAccounts()
        {
            MintApi mintApi = new MintApi();

            var accounts = mintApi.GetAccounts();

            SyncAccounts(accounts);
        }
    }
}
