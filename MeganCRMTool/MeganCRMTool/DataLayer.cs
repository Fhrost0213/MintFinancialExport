﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeganCRMTool
{
    public static class DataLayer
    {
        private static MyDbContext _db = new MyDbContext();

        public static List<T> GetList<T>() where T : class
        {
            return _db.Set<T>().ToList();
        }

        public static ObservableCollection<T> GetObservableCollection<T>() where T : class
        {
            ObservableCollection<T> coll = new ObservableCollection<T>();

            var list = _db.Set<T>().ToList();
            foreach (var item in list)
            {
                coll.Add(item);
            }

            return coll;
        }

        public static void SaveList<T>(List<T> itemList) where T : class
        {
            foreach (var item in itemList)
            {
                _db.Set<T>().AddOrUpdate(item);
            }

            _db.SaveChanges();
        }

        public static void SaveItem<T>(T item) where T : class
        {
            _db.Set<T>().AddOrUpdate(item);

            _db.SaveChanges();
        }
    }
}
