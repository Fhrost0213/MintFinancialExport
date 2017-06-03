﻿using MintFinancialExport.Core.Entities;
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

            return db.Set<T>().Where(a => a.ObjectId == objectId).First();
        }

        public static void DeleteItem<T>(int objectId) where T: class, Core.Interfaces.IObjectIdEntity
        {
            MyDbContext db = new MyDbContext();

            T instance = db.Set<T>().Where(a => a.ObjectId == objectId).First();

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

            return db.AccountMappings.Where(a => a.Account.AccountName == accountName).FirstOrDefault().AccountTypeId;
        }

        public static User GetUserFromUserName(string userName)
        {
            MyDbContext db = new MyDbContext();

            return db.Users.Where(u => u.UserName == userName).FirstOrDefault();
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

            var previousRun = GetList<AccountHistory>().OrderByDescending(a => a.RunId).Where(a => a.RunId < runId).FirstOrDefault();
            if (previousRun != null) previousRunId = previousRun.RunId;

            return previousRunId;
        }
    }
}
