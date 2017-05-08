using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Linq;
using MintFinancialExport.Core.Entities;

namespace MintFinancialExport.Data
{
    public class Export
    {
        public void ExportAccounts()
        {
            var latestRunId = DataAccess.GetList<AccountHistory>().OrderByDescending(a => a.RunId).First().RunId;
            ExportAccounts(latestRunId);
        }

        public void ExportAccounts(int? runId)
        {
            decimal? assetsTotal = 0;
            decimal? debtsTotal = 0;

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

            var assets = exportAccountList.Where(n => n.IsAsset == true && n.AccountTypeID != 99);
            var debts = exportAccountList.Where(n => n.IsAsset == false && n.AccountTypeID != 99);

            var excel = new Microsoft.Office.Interop.Excel.Application();
            var workbook = excel.Workbooks.Add();
            var worksheet = workbook.Sheets.Add();

            int iCnt = 3;

            worksheet.Cells[1, 1] = "Net Worth Statement";
            ((Range)worksheet.Cells[1, 1]).Font.Bold = true;
            ((Range)worksheet.Cells[1, 1]).Font.Underline = true;
            worksheet.Cells[3, 1] = "Assets";
            ((Range)worksheet.Cells[3, 1]).Font.Bold = true;
            ((Range)worksheet.Cells[3, 1]).Font.Underline = true;

            foreach (var asset in assets)
            {
                iCnt = iCnt + 1;

                worksheet.Cells[iCnt, 1] = asset.AccountTypeName;
                worksheet.Cells[iCnt, 2] = asset.Value;
                assetsTotal = assetsTotal + asset.Value;
            }

            iCnt = iCnt + 1;
            worksheet.Cells[iCnt, 1] = "Debts";
            ((Range)worksheet.Cells[iCnt, 1]).Font.Bold = true;
            ((Range)worksheet.Cells[iCnt, 1]).Font.Underline = true;

            foreach (var debt in debts)
            {
                iCnt = iCnt + 1;

                worksheet.Cells[iCnt, 1] = debt.AccountTypeName;
                worksheet.Cells[iCnt, 2] = debt.Value;
                debtsTotal = debtsTotal + debt.Value;
            }

            iCnt = iCnt + 1;
            worksheet.Cells[iCnt, 1] = "Total";
            ((Range)worksheet.Cells[iCnt, 1]).Font.Bold = true;
            ((Range)worksheet.Cells[iCnt, 1]).Font.Underline = true;
            worksheet.Cells[iCnt, 2] = assetsTotal + debtsTotal;
            ((Range)worksheet.Cells[iCnt, 2]).Font.Bold = true;
            ((Range)worksheet.Cells[iCnt, 2]).Font.Underline = true;

            worksheet.Columns("B").NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";

            workbook.SaveAs("C:\\test\\test.xlsx");

            workbook.Close();
        }
    }
}
