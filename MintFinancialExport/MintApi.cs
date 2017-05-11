using MintFinancialExport.Core.Entities;
using MintFinancialExport.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintFinancialExport
{
    public class MintApi
    {
        private string UserName { get; set; }
        private string Password { get; set; }

        public void GetTransactions()
        {
            SetAccountInfo();

            DateTime startDate = DateTime.Now.AddYears(-1); 
            var data = GetMintInfo("--extended-transactions --start-date " + startDate + " --include-investment " + UserName + " " + Password);
        }

        public void GetNetWorth()
        {
            //var data = GetMintInfo("--net-worth");
        }

        public ObservableCollection<MintAccount> GetAccounts()
        {
            SetAccountInfo();
            return GetAccounts(UserName, Password);
        }

        public ObservableCollection<MintAccount> GetAccounts(string userName, string password)
        {
            ObservableCollection<MintAccount> accounts = new ObservableCollection<MintAccount>();

            if (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(password))
            {
                var data = GetMintInfo("--accounts " + userName + " " + password);

                accounts = JsonConvert.DeserializeObject<ObservableCollection<MintAccount>>(data);
            }

            return accounts;
        }

        public void GetBudget()
        {
            //var data = GetMintInfo("--budgets");

            //var test = JsonConvert.DeserializeObject<Budget>(data);
        }

        public void RefreshAccounts()
        {
            var accounts = GetAccounts();

            Data.EntitySync.SyncAccounts(accounts);
        }

        public string GetMintInfo(string arguments)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = @"C:\Users\Bryan\AppData\Local\Programs\Python\Python36-32\python.exe";
            startInfo.WorkingDirectory = "C:\\windows\\system32";
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;

            startInfo.Arguments = @"C:\Users\Bryan\AppData\Local\Programs\Python\Python36-32\Scripts\mintapi-script.py " + arguments;
            process.StartInfo = startInfo;
            process.Start();

            var data = process.StandardOutput.ReadToEnd();

            process.WaitForExit();
            var exitCode = process.ExitCode;
            process.Close();

            return data;
        }

        private void SetAccountInfo()
        {
            AccountInfoView accountInfoView = new AccountInfoView();
            accountInfoView.ShowDialog();
            UserName = accountInfoView.txtUserName.Text;
            Password = accountInfoView.txtPassword.Text;
        }
    }
}
