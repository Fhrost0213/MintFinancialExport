using MintFinancialExport.Core.Entities;
using MintFinancialExport.Data;
using MintFinancialExport.ViewModels;
using MintFinancialExport.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MintFinancialExport
{
    public static class EntitySync
    {
        public static void SyncAccounts(ObservableCollection<MintAccount> accountlist)
        {
            int? nextRunId = 0;
            var latestRun = DataAccess.GetList<AccountHistory>().OrderByDescending(a => a.RunId).FirstOrDefault();
            if (latestRun != null) nextRunId = latestRun.RunId + 1;
            DateTime asOfDate = System.DateTime.Now;

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

            // Prompt for manual values
            var accounts = DataAccess.GetList<Account>();
            var manualAccounts = accounts.FindAll(m => m.IsManual == true);

            foreach (var account in manualAccounts)
            {
                ManualAccountView view = new ManualAccountView();
                ManualAccountViewModel model = new ManualAccountViewModel();
                view.DataContext = model;
                model.AccountName = account.AccountName;

                var previousHistory = DataAccess.GetList<AccountHistory>().Where(a => a.AccountId == account.ObjectId).OrderByDescending(r => r.RunId).FirstOrDefault();

                if (previousHistory != null) model.Value = previousHistory.Amount;
 
                view.ShowDialog();

                AccountHistory accountHistory = new AccountHistory();
                accountHistory.Account = account;
                accountHistory.AccountId = account.ObjectId;
                accountHistory.Amount = model.Value;
                accountHistory.AsOfDate = asOfDate;
                accountHistory.RunId = nextRunId;

                DataAccess.SaveItem(accountHistory);
            }

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
