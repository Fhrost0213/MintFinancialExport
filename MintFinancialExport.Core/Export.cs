using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Interop;
using MintFinancialExport.Core.Entities;

namespace MintFinancialExport.Core
{
    public class Export
    {
        private int _assetsStart;
        private int _debtsStart;
        private int _totalStart;

        private string _numberFormat = "$#,##0.00_);[Red]($#,##0.00)";
        private string _amountChangeAssetsFormat = "$#,##0.00_);[Red]($#,##0.00)";
        private string _amountChangeDebtsFormat = "[Red]$#,##0.00_);($#,##0.00)";
        private string _totalsFormat = "$#,##0.00_);[Red]($#,##0.00)";
        private string _percentFormat = "0.00%;[Red](0.00%)";

        private IDataAccess _dataAccess;

        public Export()
        {
            _dataAccess = ServiceLocator.GetInstance<IDataAccess>();
        }

        public void ExportAccounts(List<ExportAccount> exportAccountList, string filePath, List<ExportAccount> compareAccountList = null)
        {
            int exportAccountListColumn = 2;
            int compareAccountListColumn = 0;
            if (compareAccountList != null)
            {
                compareAccountListColumn = 2;
                exportAccountListColumn = 3;
            }

            var excel = new Application();
            var workbook = excel.Workbooks.Add();
            var worksheet = workbook.Sheets.Add();

            SetHeader(worksheet);

            if (compareAccountList != null) WriteListValues(compareAccountListColumn, compareAccountList, worksheet);
            WriteListValues(exportAccountListColumn, exportAccountList, worksheet);

            if (compareAccountList != null) WriteDifferences(worksheet);

            workbook.SaveAs(filePath);

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

            worksheet.Cells[1, iColumn] = "Change ($)";
            ((Range)worksheet.Cells[1, iColumn]).Font.Bold = true;
            ((Range)worksheet.Cells[1, iColumn]).HorizontalAlignment = XlHAlign.xlHAlignRight;
            ((Range)worksheet.Cells[1, iColumn]).Borders.Weight = 2d;
            ((Range)worksheet.Cells[1, iColumn]).Borders.LineStyle = XlLineStyle.xlContinuous;

            while (iCnt < _totalStart + 1)
            {
                iCnt++;

                worksheet.Cells[iCnt, iColumn] = worksheet.Cells[iCnt, iColumn - 1].Value2 - worksheet.Cells[iCnt, iColumn - 2].Value2;

                if (iCnt < _debtsStart + 1 || iCnt >= _totalStart)
                {
                    worksheet.Cells[iCnt, iColumn].NumberFormat = _amountChangeAssetsFormat;
                }
                else
                {
                    worksheet.Cells[iCnt, iColumn].NumberFormat = _amountChangeDebtsFormat;
                }
            }
        }

        private void WriteDeltaDifferences(dynamic worksheet)
        {
            int iCnt = _assetsStart;
            int iColumn = 5;

            worksheet.Cells[1, iColumn] = "Change (%)";
            ((Range)worksheet.Cells[1, iColumn]).Font.Bold = true;
            ((Range)worksheet.Cells[1, iColumn]).HorizontalAlignment = XlHAlign.xlHAlignRight;
            ((Range)worksheet.Cells[1, iColumn]).Borders.Weight = 2d;
            ((Range)worksheet.Cells[1, iColumn]).Borders.LineStyle = XlLineStyle.xlContinuous;

            while (iCnt < _totalStart + 1)
            {
                iCnt++;

                if (worksheet.Cells[iCnt, iColumn - 3].Value2 == 0)
                {
                    worksheet.Cells[iCnt, iColumn] = 0;
                }
                else
                {
                    worksheet.Cells[iCnt, iColumn] = (worksheet.Cells[iCnt, iColumn - 2].Value2 - worksheet.Cells[iCnt, iColumn - 3].Value2) / worksheet.Cells[iCnt, iColumn - 3].Value2;
                }

                worksheet.Cells[iCnt, iColumn].NumberFormat = _percentFormat;
            }
        }

        private void WriteListValues(int iColumn, IEnumerable<ExportAccount> list, dynamic worksheet)
        {
            decimal? assetsTotal = 0;
            decimal? debtsTotal = 0;

            worksheet.Cells[1, iColumn] = list.FirstOrDefault().AsOfDate;
            ((Range)worksheet.Cells[1, iColumn]).Font.Bold = true;
            ((Range)worksheet.Cells[1, iColumn]).Borders.Weight = 2d;
            ((Range)worksheet.Cells[1, iColumn]).Borders.LineStyle = XlLineStyle.xlContinuous;

            var assets = list.Where(n => n.IsAsset == true && n.AccountTypeID != 99);
            var debts = list.Where(n => n.IsAsset == false && n.AccountTypeID != 99);

            int iCnt = _assetsStart;

            // Write individual asset values
            foreach (var asset in assets)
            {
                iCnt++;

                worksheet.Cells[iCnt, iColumn] = asset.Value;
                worksheet.Cells[iCnt, iColumn].NumberFormat = _numberFormat;
                assetsTotal = assetsTotal + asset.Value;
            }

            iCnt++;
            // Assets Total
            worksheet.Cells[iCnt, iColumn] = assetsTotal;
            worksheet.Cells[iCnt, iColumn].NumberFormat = _totalsFormat;
            ((Range)worksheet.Cells[iCnt, iColumn]).Font.Bold = true;
            ((Range)worksheet.Cells[iCnt, iColumn]).Font.Underline = true;

            iCnt = _debtsStart;

            // Write individual debt values
            foreach (var debt in debts)
            {
                iCnt++;

                worksheet.Cells[iCnt, iColumn] = debt.Value;
                worksheet.Cells[iCnt, iColumn].NumberFormat = _numberFormat;
                debtsTotal = debtsTotal + debt.Value;
            }

            iCnt++;

            // Debts Total
            worksheet.Cells[iCnt, iColumn] = debtsTotal;
            worksheet.Cells[iCnt, iColumn].NumberFormat = _totalsFormat;
            ((Range)worksheet.Cells[iCnt, iColumn]).Font.Bold = true;
            ((Range)worksheet.Cells[iCnt, iColumn]).Font.Underline = true;

            iCnt = _totalStart;

            // Net Worth Total
            worksheet.Cells[iCnt, iColumn] = assetsTotal + debtsTotal;
            worksheet.Cells[iCnt, iColumn].NumberFormat = _numberFormat;
            ((Range)worksheet.Cells[iCnt, iColumn]).Font.Bold = true;
            ((Range)worksheet.Cells[iCnt, iColumn]).Font.Underline = true;
        }

        private void SetHeader(dynamic worksheet)
        {
            int iHeaderColumn = 1;
            int iCnt = 1;

            worksheet.Columns("A").ColumnWidth = 30;
            worksheet.Columns("B").ColumnWidth = 17;
            worksheet.Columns("C").ColumnWidth = 17;
            worksheet.Columns("D").ColumnWidth = 17;
            worksheet.Columns("E").ColumnWidth = 17;

            worksheet.Cells[1, iHeaderColumn] = "Net Worth Statement";
            ((Range)worksheet.Cells[1, iHeaderColumn]).Font.Bold = true;
            ((Range)worksheet.Cells[1, iHeaderColumn]).Font.Underline = true;

            //worksheet.Cells[3, iHeaderColumn] = "Assets";
            //((Range)worksheet.Cells[3, iHeaderColumn]).Font.Bold = true;
            //((Range)worksheet.Cells[3, iHeaderColumn]).Font.Underline = true;

            var types = _dataAccess.GetList<AccountType>().Where(t => t.ObjectId != 99);

            var assetTypes = types.Where(t => t.IsAsset == true);
            var debtTypes = types.Where(t => t.IsAsset == false);

            _assetsStart = iCnt;

            foreach (var type in assetTypes)
            {
                iCnt++;
                worksheet.Cells[iCnt, iHeaderColumn] = type.AccountTypeDesc;    
            }

            iCnt++;
            worksheet.Cells[iCnt, iHeaderColumn] = "ASSETS TOTAL";
            ((Range)worksheet.Rows[iCnt]).Font.Bold = true;
            ((Range)worksheet.Rows[iCnt]).Font.Underline = true;
            //iCnt++; // Add space

            //worksheet.Cells[iCnt, 1] = "Debts";
            //((Range)worksheet.Cells[iCnt, 1]).Font.Bold = true;
            //((Range)worksheet.Cells[iCnt, 1]).Font.Underline = true;

            _debtsStart = iCnt;

            foreach (var type in debtTypes)
            {
                iCnt++;
                worksheet.Cells[iCnt, iHeaderColumn] = type.AccountTypeDesc;
            }

            iCnt++;
            worksheet.Cells[iCnt, iHeaderColumn] = "DEBTS TOTAL";
            ((Range)worksheet.Rows[iCnt]).Font.Bold = true;
            ((Range)worksheet.Rows[iCnt]).Font.Underline = true;
            iCnt++; // Add space

            _totalStart = iCnt;

            worksheet.Cells[iCnt, 1] = "NET WORTH";
            ((Range)worksheet.Rows[iCnt]).Font.Bold = true;
            ((Range)worksheet.Rows[iCnt]).Font.Underline = true;
        }
    }
}
