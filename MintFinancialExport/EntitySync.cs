using MintFinancialExport.Core.Entities;
using MintFinancialExport.Data;
using MintFinancialExport.ViewModels;
using MintFinancialExport.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintFinancialExport
{
    public static class EntitySync
    {
        public static void SyncAccounts(ObservableCollection<MintAccount> accountlist)
        {
            MyDbContext db = new MyDbContext();

            var nextRunID = db.AccountHistories.OrderByDescending(a => a.RunId).FirstOrDefault().RunId + 1;
            DateTime asOfDate = System.DateTime.Now;

            // Sync accounts with DB
            foreach (var account in accountlist)
            {
                Account accountItem = db.Accounts.FirstOrDefault(n => n.AccountName.Equals(account.Name));
                if (accountItem == null)
                {
                    accountItem = new Account();
                    accountItem.AccountName = account.Name;
                }

                db.Accounts.AddOrUpdate(accountItem);
                db.SaveChanges();

                // Store off a snapshot of account history
                AccountHistory accountHistory = new AccountHistory();
                accountHistory.Account = accountItem;
                accountHistory.AccountId = accountItem.AccountId;
                accountHistory.Amount = account.Value;
                accountHistory.AsOfDate = asOfDate;
                accountHistory.RunId = nextRunID;
                db.AccountHistories.Add(accountHistory);

                db.SaveChanges();
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

                var previousHistory = db.AccountHistories.Where(a => a.AccountId == account.AccountId).OrderByDescending(r => r.RunId).FirstOrDefault();
                if (previousHistory != null) model.Value = previousHistory.Amount;
 
                view.ShowDialog();

                AccountHistory accountHistory = new AccountHistory();
                accountHistory.Account = account;
                accountHistory.AccountId = account.AccountId;
                accountHistory.Amount = model.Value;
                accountHistory.AsOfDate = asOfDate;
                accountHistory.RunId = nextRunID;
                db.AccountHistories.Add(accountHistory);

                db.SaveChanges();
            }
        }
    }
}
