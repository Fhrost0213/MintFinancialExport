using Microsoft.Office.Interop.Excel;
using MintFinancialExport.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MintFinancialExport
{
    class Export
    {
        public void ExportToExcel(ObservableCollection<Account> accounts, double physicalAssetsAmount, double mortgageAmount)
        {
            double assetsTotal = 0;
            double debtsTotal = 0;
            double total = 0;
            //int iCnt = 1;
            var excel = new Microsoft.Office.Interop.Excel.Application();

            var workbook = excel.Workbooks.Add();

            var worksheet = workbook.Sheets.Add();

            worksheet.Columns("B").NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";

            worksheet.Cells[1, 1] = "Personal Balance Sheet";
            ((Range)worksheet.Cells[1, 1]).Font.Bold = true;
            ((Range)worksheet.Cells[1, 1]).Font.Underline = true;

            worksheet.Cells[1, 2] = DateTime.Now.ToShortDateString();

            #region "Assets"
            // Cash & Savings
            worksheet.Cells[2, 1] = "Cash & Savings (includes HSA cash)";
            var names = new string[] { "Investor Checking", "HSA", "Savings" };
            total = GetTotal(accounts.Where(n => names.Contains(n.Name)));
            assetsTotal = assetsTotal + total;
            worksheet.Cells[2, 2] = total;
            
            // Traditional 401k / IRA
            worksheet.Cells[3, 1] = "Traditional 401(k) / IRA";
            names = new string[] { "MARKIT 401(K)", "SOUTHWEST DIAGNOSTIC IMAGING CENTER 401(K) RETIREMENT PLAN", "Traditional SIMPLE IRA - Target Date 2045" };
            total = GetTotal(accounts.Where(n => names.Contains(n.Name)));
            assetsTotal = assetsTotal + total;
            worksheet.Cells[3, 2] = total;

            // Roth IRA
            worksheet.Cells[4, 1] = "Roth IRA";
            total = GetTotal(accounts.Where(n => n.Name.Equals("Roth Contributory IRA")));
            assetsTotal = assetsTotal + total;
            worksheet.Cells[4, 2] = total;

            // HSA
            worksheet.Cells[5, 1] = "Health Savings Account";
            total = GetTotal(accounts.Where(n => n.Name.Equals("HSA Investment Account")));
            assetsTotal = assetsTotal + total;
            worksheet.Cells[5, 2] = total;

            worksheet.Cells[6, 1] = "Stock Option";

            // Brokerage
            worksheet.Cells[7, 1] = "Brokerage";
            total = GetTotal(accounts.Where(n => n.Name.Equals("Joint Brokerage")));
            assetsTotal = assetsTotal + total;
            worksheet.Cells[7, 2] = total; 

            // Real Estate
            worksheet.Cells[8, 1] = "Real Estate (market value)";
            total = GetTotal(accounts.Where(n => n.Name.Equals("315 Highland Glen Dr")));
            assetsTotal = assetsTotal + total;
            worksheet.Cells[8, 2] = total;

            worksheet.Cells[9, 1] = "Websites";
            worksheet.Cells[10, 1] = "Physical Assets";
            assetsTotal = assetsTotal + physicalAssetsAmount;
            worksheet.Cells[10, 2] = physicalAssetsAmount;

            // Automobiles
            worksheet.Cells[11, 1] = "Automobile (KBB value)";
            names = new string[] { "My Hyundai Sonata", "My Kia Sportage" };
            total = GetTotal(accounts.Where(n => names.Contains(n.Name)));
            assetsTotal = assetsTotal + total;
            worksheet.Cells[11, 2] = total;

            // Assets Total
            worksheet.Cells[12, 1] = "Assets Total";
            ((Range)worksheet.Cells[12, 1]).Font.Bold = true;
            ((Range)worksheet.Cells[12, 1]).Font.Underline = true;
            worksheet.Cells[12, 2] = assetsTotal;
            #endregion

            // Debts
            #region "Debts"
            worksheet.Cells[13, 1] = "Credit Cards";
            total = GetTotal(accounts.Where(n => n.Name.Equals("BankAmericard Rewards Visa Platinum Plus")));
            debtsTotal = debtsTotal + total;
            worksheet.Cells[13, 2] = total;

            if (mortgageAmount >= 0) mortgageAmount = -mortgageAmount;
            worksheet.Cells[14, 1] = "Student Loans";
            worksheet.Cells[15, 1] = "Mortgages";
            debtsTotal = debtsTotal + mortgageAmount;
            worksheet.Cells[15, 2] = mortgageAmount;

            // Debts Total
            worksheet.Cells[16, 1] = "Debts Total";
            ((Range)worksheet.Cells[16, 1]).Font.Bold = true;
            ((Range)worksheet.Cells[16, 1]).Font.Underline = true;
            worksheet.Cells[16, 2] = debtsTotal;

            #endregion
            worksheet.Cells[17, 1] = "Net Worth";
            ((Range)worksheet.Cells[17, 1]).Font.Bold = true;
            ((Range)worksheet.Cells[17, 1]).Font.Underline = true;
            worksheet.Cells[17, 2] = assetsTotal + debtsTotal;



            //foreach (var account in accounts)
            //{
            //    iCnt = iCnt + 1;
            //    worksheet.Cells[iCnt, 1] = account.Name;
            //    worksheet.Cells[iCnt, 2] = account.Value;
            //}

            workbook.SaveAs("C:\\test\\test.xlsx");

            workbook.Close();
        }

        public double GetTotal(IEnumerable<Account> accounts)
        {
            double total = 0;

            foreach (Account account in accounts)
            {
                total = total + account.Value;
            }

            return total;
        }
    }
}
