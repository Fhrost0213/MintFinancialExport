using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace MintFinancialExport.Core
{
    public class DataAccess : IDataAccess
    {
        public void SaveList<T>(List<T> itemList) where T : class
        {
            MyDbContext db = new MyDbContext();

            foreach (var item in itemList)
            {
                db.Set<T>().AddOrUpdate(item);
            }

            db.SaveChanges();
        }

        public void SaveItem<T>(T item) where T : class
        {
            MyDbContext db = new MyDbContext();
            db.Set<T>().AddOrUpdate(item);

            db.SaveChanges();
        }

        public List<T> GetList<T>() where T : class
        {
            MyDbContext db = new MyDbContext();
            return db.Set<T>().ToList();
        }

        public T GetItem<T>(int objectId) where T: class, Core.Interfaces.IObjectIdEntity
        {
            MyDbContext db = new MyDbContext();

            return db.Set<T>().First(a => a.ObjectId == objectId);
        }

        public void DeleteItem<T>(int objectId) where T: class, Core.Interfaces.IObjectIdEntity
        {
            MyDbContext db = new MyDbContext();

            T instance = db.Set<T>().First(a => a.ObjectId == objectId);

            db.Set<T>().Remove(instance);

            db.SaveChanges();
        }

        public void DeleteItem<T>(T item) where T : class
        {
            MyDbContext db = new MyDbContext();

            db.Set<T>().Remove(item);

            db.SaveChanges();
        }

        public bool DoesItemExist<T>(int objectId) where T : class, Core.Interfaces.IObjectIdEntity
        {
            MyDbContext db = new MyDbContext();
            return db.Set<T>().Any(a => a.ObjectId == objectId);
        }

        public bool DoesItemExist<T>(T item) where T : class, Core.Interfaces.IObjectIdEntity
        {
            MyDbContext db = new MyDbContext();
            return db.Set<T>().Any(i => i.ObjectId == item.ObjectId);
        }

        public int? GetAccountTypeIdFromAccountName(string accountName)
        {
            MyDbContext db = new MyDbContext();

            return db.AccountMappings.FirstOrDefault(a => a.Account.AccountName == accountName)?.AccountTypeId;
        }

        public User GetUserFromUserName(string userName)
        {
            MyDbContext db = new MyDbContext();

            return db.Users.FirstOrDefault(u => u.UserName == userName);
        }

        public int? GetNextRunId()
        {
            return GetCurrentRunId() + 1;
        }

        public int? GetCurrentRunId()
        {
            int? currentRunId = 0;
            var latestRun = GetList<AccountHistory>().OrderByDescending(a => a.RunId).FirstOrDefault();
            if (latestRun != null) currentRunId = latestRun.RunId;

            return currentRunId;
        }

        public int? GetPreviousRunId(int? runId)
        {
            int? previousRunId = 0;

            var previousRun = GetList<AccountHistory>().OrderByDescending(a => a.RunId).FirstOrDefault(a => a.RunId < runId);
            if (previousRun != null) previousRunId = previousRun.RunId;

            return previousRunId;
        }

        public string GetOption(string optionName)
        {
            MyDbContext db = new MyDbContext();

            return db.Options.FirstOrDefault(o => o.OptionKey == optionName)?.OptionValue;
        }

        public void SaveOption(string optionKey, string optionValue)
        {
            MyDbContext db = new MyDbContext();

            Option option = db.Options.FirstOrDefault(o => o.OptionKey == optionKey) ?? new Option();

            option.OptionKey = optionKey;
            option.OptionValue = optionValue;

            db.Set<Option>().AddOrUpdate(option);

            db.SaveChanges();
        }
    }
}
