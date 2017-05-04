using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintFinancialExport
{
    public static class DataAccess
    {
        public static void SyncAccounts(ObservableCollection<Entities.Account> accountlist)
        {
            MyDbContext db = new MyDbContext();

            // Sync accounts with DB
            foreach (var account in accountlist)
            {
                var accountItem = db.Accounts.FirstOrDefault(n => n.AccountName.Equals(account.Name));
                db.Accounts.AddOrUpdate(accountItem);

                // Store off a snapshot of account history
                AccountHistory accountHistory = new AccountHistory();
                accountHistory.Account = accountItem;
                accountHistory.AccountId = accountItem.AccountId;
                accountHistory.Amount = account.Value;
                accountHistory.AsOfDate = System.DateTime.Now;
                db.AccountHistories.Add(accountHistory);

                db.SaveChanges();
            }
        }

        public static List<AccountMapping> GetAccountMappings()
        {
            MyDbContext db = new MyDbContext();
            return db.AccountMappings.ToList();
        }

        public static List<AccountType> GetAccountTypes()
        {
            MyDbContext db = new MyDbContext();
            return db.AccountTypes.ToList();
        }

        public static void SaveAccountMappings(List<AccountMapping> accountMappingList)
        {
            MyDbContext db = new MyDbContext();

            foreach (var accountMapping in accountMappingList)
            {
                db.AccountMappings.AddOrUpdate(accountMapping);
            }
        }
    }
}
