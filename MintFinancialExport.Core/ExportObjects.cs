using MintFinancialExport.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintFinancialExport.Core
{
    public class ExportObjects
    {
        public List<ExportAccount> GetExportAccountList()
        {
            return GetExportAccountList(DataAccess.GetCurrentRunId());
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
                    var typeId = (int)mapping.AccountTypeId;

                    var account = exportAccountList.First(i => i.AccountTypeID == typeId);
                    account.Value += item.Amount;
                    account.AsOfDate = item.AsOfDate;
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
