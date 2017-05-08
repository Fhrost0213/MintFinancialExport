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
