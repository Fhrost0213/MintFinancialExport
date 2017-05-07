using MintFinancialExport.Core.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.Linq;

namespace MintFinancialExport.Data
{
    public static class DataAccess
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
        }

        public static void SaveList<T>(List<T> itemList) where T : class
        {
            MyDbContext db = new MyDbContext();

            foreach (var item in itemList)
            {
                db.Set<T>().AddOrUpdate(item);
            }

            db.SaveChanges();
        }

        public static List<T> GetList<T>() where T: class
        {
            MyDbContext db = new MyDbContext();
            return db.Set<T>().ToList();
        }

        public static int? GetAccountTypeIdFromAccountName(string accountName)
        {
            MyDbContext db = new MyDbContext();

            return db.AccountMappings.Where(a => a.Account.AccountName == accountName).FirstOrDefault().AccountTypeId;
        }
    }
}
