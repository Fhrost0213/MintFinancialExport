using MintFinancialExport.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MintFinancialExport.Core
{
    public class MintApi
    {
        public void GetTransactions()
        {
            DateTime startDate = DateTime.Now.AddYears(-1); 
            var data = GetMintInfo("--extended-transactions --start-date " + startDate + " --include-investment " + AccountInfo.UserName + " " + AccountInfo.Password);
        }

        public void GetNetWorth()
        {
            //var data = GetMintInfo("--net-worth");
        }

        public ObservableCollection<MintAccount> GetAccounts()
        {
            return GetAccounts(AccountInfo.UserName, AccountInfo.Password);
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

        public string GetMintInfo(string arguments)
        {
            string pythonFolderLocation = DataAccess.GetOption(Enums.Options.PythonFolderLocation.ToString());

            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = pythonFolderLocation + @"\python.exe";
            startInfo.WorkingDirectory = "C:\\windows\\system32";
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;

            startInfo.Arguments = pythonFolderLocation + @"\Scripts\mintapi-script.py " + arguments;
            process.StartInfo = startInfo;
            process.Start();

            var data = process.StandardOutput.ReadToEnd();

            process.WaitForExit();
            var exitCode = process.ExitCode;
            process.Close();

            return data;
        }
    }
}
