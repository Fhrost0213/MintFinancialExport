using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace MintFinancialExport.Core
{
    public static class DataAccess
    {
        public static void SaveList<T>(List<T> itemList) where T : class
        {
            MyDbContext db = new MyDbContext();

            foreach (var item in itemList)
            {
                db.Set<T>().AddOrUpdate(item);
            }

            db.SaveChanges();
        }

        public static void SaveItem<T>(T item) where T : class
        {
            MyDbContext db = new MyDbContext();
            db.Set<T>().AddOrUpdate(item);

            db.SaveChanges();
        }

        public static List<T> GetList<T>() where T : class
        {
            MyDbContext db = new MyDbContext();
            return db.Set<T>().ToList();
        }

        public static T GetItem<T>(int objectId) where T: class, Core.Interfaces.IObjectIdEntity
        {
            MyDbContext db = new MyDbContext();

            return db.Set<T>().First(a => a.ObjectId == objectId);
        }

        public static void DeleteItem<T>(int objectId) where T: class, Core.Interfaces.IObjectIdEntity
        {
            MyDbContext db = new MyDbContext();

            T instance = db.Set<T>().First(a => a.ObjectId == objectId);

            db.Set<T>().Remove(instance);

            db.SaveChanges();
        }

        public static void DeleteItem<T>(T item) where T : class
        {
            MyDbContext db = new MyDbContext();

            db.Set<T>().Remove(item);

            db.SaveChanges();
        }

        public static bool DoesItemExist<T>(int objectId) where T : class, Core.Interfaces.IObjectIdEntity
        {
            MyDbContext db = new MyDbContext();
            return db.Set<T>().Any(a => a.ObjectId == objectId);
        }

        public static bool DoesItemExist<T>(T item) where T : class, Core.Interfaces.IObjectIdEntity
        {
            MyDbContext db = new MyDbContext();
            return db.Set<T>().Any(i => i.ObjectId == item.ObjectId);
        }

        public static int? GetAccountTypeIdFromAccountName(string accountName)
        {
            MyDbContext db = new MyDbContext();

            return db.AccountMappings.FirstOrDefault(a => a.Account.AccountName == accountName)?.AccountTypeId;
        }

        public static User GetUserFromUserName(string userName)
        {
            MyDbContext db = new MyDbContext();

            return db.Users.FirstOrDefault(u => u.UserName == userName);
        }

        public static int? GetNextRunId()
        {
            return GetCurrentRunId() + 1;
        }

        public static int? GetCurrentRunId()
        {
            int? currentRunId = 0;
            var latestRun = DataAccess.GetList<AccountHistory>().OrderByDescending(a => a.RunId).FirstOrDefault();
            if (latestRun != null) currentRunId = latestRun.RunId;

            return currentRunId;
        }

        public static int? GetPreviousRunId(int? runId)
        {
            int? previousRunId = 0;

            var previousRun = GetList<AccountHistory>().OrderByDescending(a => a.RunId).FirstOrDefault(a => a.RunId < runId);
            if (previousRun != null) previousRunId = previousRun.RunId;

            return previousRunId;
        }
    }
}
