﻿using MintFinancialExport.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintFinancialExport.Data
{
    public class ExportObjects
    {
        public List<ExportAccount> GetExportAccountList()
        {
            var latestRunId = DataAccess.GetList<AccountHistory>().OrderByDescending(a => a.RunId).First().RunId;
            return GetExportAccountList(latestRunId);
        }

        public List<ExportAccount> GetExportAccountList(int? runId)
        {
            List<ExportAccount> exportAccountList = new List<ExportAccount>();

            List<AccountType> types = DataAccess.GetList<AccountType>();

            foreach (AccountType type in types)
            {
                ExportAccount exportAccount = new ExportAccount();
                exportAccount.AccountTypeID = type.ObjectId;
                exportAccount.AccountTypeName = type.AccountTypeDesc;
                exportAccount.IsAsset = type.IsAsset;
                exportAccount.Value = 0;
                exportAccountList.Add(exportAccount);
            }

            var accountHistoryList = DataAccess.GetList<AccountHistory>().Where(a => a.RunId.Equals(runId));

            foreach (var item in accountHistoryList)
            {
                if (item.Account.AccountMappings.Count != 0)
                {
                    var mapping = item.Account.AccountMappings.First();
                    var typeID = (int)mapping.AccountTypeId;

                    var value = exportAccountList.Where(i => i.AccountTypeID == typeID).First();
                    value.Value = value.Value + item.Amount;
                }
                else
                {
                    //throw new Exception(item.Account.AccountName + " is not mapped. If you do not wish to include this account in the statement, then map it to Account Type = None");
                }
            }

            return exportAccountList;
        }
    }
}