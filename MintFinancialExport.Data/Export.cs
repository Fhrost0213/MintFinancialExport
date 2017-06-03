using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Linq;
using MintFinancialExport.Core.Entities;
using System;
using Microsoft.Win32;

namespace MintFinancialExport.Data
{
    public class Export
    {
        private int _assetsStart;
        private int _debtsStart;
        private int _totalStart;

        public void ExportAccounts(List<ExportAccount> exportAccountList, List<ExportAccount> compareAccountList = null)
        {
            int exportAccountListColumn = 2;
            int compareAccountListColumn = 0;
            if (compareAccountList != null)
            {
                compareAccountListColumn = 2;
                exportAccountListColumn = 3;
            }

            var excel = new Microsoft.Office.Interop.Excel.Application();
            var workbook = excel.Workbooks.Add();
            var worksheet = workbook.Sheets.Add();

            SetHeader(worksheet);

            if (compareAccountList != null) WriteListValues(compareAccountListColumn, compareAccountList, worksheet);
            WriteListValues(exportAccountListColumn, exportAccountList, worksheet);

            if (compareAccountList != null) WriteDifferences(worksheet);

            workbook.SaveAs(GetSaveFilePath());

            workbook.Close();
        }

        private void WriteDifferences(dynamic worksheet)
        {
            WriteAmountDifferences(worksheet);
            WriteDeltaDifferences(worksheet);
        }

        private void WriteAmountDifferences(dynamic worksheet)
        {
            int iCnt = _assetsStart;
            int iColumn = 4;

            worksheet.Cells[2, iColumn] = "Amount Difference";
            ((Range)worksheet.Cells[2, iColumn]).Font.Bold = true;
            ((Range)worksheet.Cells[2, iColumn]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            while (iCnt < _debtsStart - 2)
            {
                iCnt++;

                worksheet.Cells[iCnt, iColumn] = worksheet.Cells[iCnt, iColumn - 1].Value2 - worksheet.Cells[iCnt, iColumn - 2].Value2;
                worksheet.Cells[iCnt, iColumn].NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
            }

            while (iCnt < _totalStart - 2)
            {
                iCnt++;

                worksheet.Cells[iCnt, iColumn] = worksheet.Cells[iCnt, iColumn - 1].Value2 - worksheet.Cells[iCnt, iColumn - 2].Value2;
                worksheet.Cells[iCnt, iColumn].NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
            }

            worksheet.Cells[_totalStart, iColumn] = worksheet.Cells[_totalStart, iColumn - 1].Value2 - worksheet.Cells[_totalStart, iColumn - 2].Value2;
            worksheet.Cells[_totalStart, iColumn].NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
        }

        private void WriteDeltaDifferences(dynamic worksheet)
        {
            int iCnt = _assetsStart;
            int iColumn = 5;

            worksheet.Cells[2, iColumn] = "Delta";
            ((Range)worksheet.Cells[2, iColumn]).Font.Bold = true;
            ((Range)worksheet.Cells[2, iColumn]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            while (iCnt < _debtsStart - 2)
            {
                iCnt++;

                worksheet.Cells[iCnt, iColumn] = (worksheet.Cells[iCnt, iColumn - 2].Value2 - worksheet.Cells[iCnt, iColumn - 3].Value2) / worksheet.Cells[iCnt, iColumn - 3].Value2;
                worksheet.Cells[iCnt, iColumn].NumberFormat = "0.00%";
            }

            while (iCnt < _totalStart - 2)
            {
                iCnt++;

                worksheet.Cells[iCnt, iColumn] = (worksheet.Cells[iCnt, iColumn - 2].Value2 - worksheet.Cells[iCnt, iColumn - 3].Value2) / worksheet.Cells[iCnt, iColumn - 3].Value2;
                worksheet.Cells[iCnt, iColumn].NumberFormat = "0.00%";
            }

            worksheet.Cells[_totalStart, iColumn] = (worksheet.Cells[_totalStart, iColumn - 2].Value2 - worksheet.Cells[_totalStart, iColumn - 3].Value2) / worksheet.Cells[_totalStart, iColumn - 3].Value2;
            worksheet.Cells[_totalStart, iColumn].NumberFormat = "0.00%";
        }

        private void WriteListValues(int iColumn, IEnumerable<ExportAccount> list, dynamic worksheet)
        {
            decimal? assetsTotal = 0;
            decimal? debtsTotal = 0;

            worksheet.Cells[2, iColumn] = list.FirstOrDefault().AsOfDate;
            ((Range)worksheet.Cells[2, iColumn]).Font.Bold = true;

            var assets = list.Where(n => n.IsAsset == true && n.AccountTypeID != 99);
            var debts = list.Where(n => n.IsAsset == false && n.AccountTypeID != 99);

            int iCnt = _assetsStart;

            foreach (var asset in assets)
            {
                iCnt++;

                worksheet.Cells[iCnt, iColumn] = asset.Value;
                worksheet.Cells[iCnt, iColumn].NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                assetsTotal = assetsTotal + asset.Value;
            }

            iCnt = _debtsStart;

            foreach (var debt in debts)
            {
                iCnt++;

                worksheet.Cells[iCnt, iColumn] = debt.Value;
                worksheet.Cells[iCnt, iColumn].NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                debtsTotal = debtsTotal + debt.Value;
            }

            iCnt = _totalStart;

            worksheet.Cells[iCnt, iColumn] = assetsTotal + debtsTotal;
            worksheet.Cells[iCnt, iColumn].NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
            ((Range)worksheet.Cells[iCnt, iColumn]).Font.Bold = true;
            ((Range)worksheet.Cells[iCnt, iColumn]).Font.Underline = true;
        }

        private void SetHeader(dynamic worksheet)
        {
            int iHeaderColumn = 1;
            int iCnt = 3;

            worksheet.Columns("A").ColumnWidth = 25;
            worksheet.Columns("B").ColumnWidth = 17;
            worksheet.Columns("C").ColumnWidth = 17;
            worksheet.Columns("D").ColumnWidth = 17;

            worksheet.Cells[1, iHeaderColumn] = "Net Worth Statement";
            ((Range)worksheet.Cells[1, iHeaderColumn]).Font.Bold = true;
            ((Range)worksheet.Cells[1, iHeaderColumn]).Font.Underline = true;

            worksheet.Cells[3, iHeaderColumn] = "Assets";
            ((Range)worksheet.Cells[3, iHeaderColumn]).Font.Bold = true;
            ((Range)worksheet.Cells[3, iHeaderColumn]).Font.Underline = true;

            var types = DataAccess.GetList<AccountType>().Where(t => t.ObjectId != 99);

            var assetTypes = types.Where(t => t.IsAsset == true);
            var debtTypes = types.Where(t => t.IsAsset == false);

            _assetsStart = iCnt;

            foreach (var type in assetTypes)
            {
                iCnt++;
                worksheet.Cells[iCnt, iHeaderColumn] = type.AccountTypeDesc;
                
            }

            iCnt++; // Add space
            iCnt++; // Add space

            worksheet.Cells[iCnt, 1] = "Debts";
            ((Range)worksheet.Cells[iCnt, 1]).Font.Bold = true;
            ((Range)worksheet.Cells[iCnt, 1]).Font.Underline = true;

            _debtsStart = iCnt;

            foreach (var type in debtTypes)
            {
                iCnt++;
                worksheet.Cells[iCnt, iHeaderColumn] = type.AccountTypeDesc;
            }

            iCnt++; // Add space
            iCnt++; // Add space

            _totalStart = iCnt;

            worksheet.Cells[iCnt, 1] = "Total";
            ((Range)worksheet.Cells[iCnt, 1]).Font.Bold = true;
            ((Range)worksheet.Cells[iCnt, 1]).Font.Underline = true;
        }

        private string GetSaveFilePath()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = "NetWorthStatement_" + DateTime.Now.ToShortDateString().Replace("/", "_") + ".xlsx";
            dialog.Filter = "Excel (*.xlsx)|*.xlsx";

            dialog.ShowDialog();

            return dialog.FileName;
        }
    }
}
