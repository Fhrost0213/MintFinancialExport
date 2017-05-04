using MintFinancialExport.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintFinancialExport.Models
{
    public class MintFinancialExportModel
    {
        public void GetTransactions(string userName, string password)
        {
            DateTime startDate = DateTime.Now.AddYears(-1); 
            var data = GetMintInfo("--extended-transactions --start-date " + startDate + " --include-investment " + userName + " " + password);
        }

        public void GetNetWorth()
        {
            //var data = GetMintInfo("--net-worth");
        }

        public ObservableCollection<Entities.Account> GetAccounts(string userName, string password)
        {
            var data = GetMintInfo("--accounts " + userName + " " + password);

            return JsonConvert.DeserializeObject<ObservableCollection<Entities.Account>>(data);
        }

        public void GetBudget()
        {
            //var data = GetMintInfo("--budgets");

            //var test = JsonConvert.DeserializeObject<Budget>(data);
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

            startInfo.Arguments = @"C:\Users\Bryan\AppData\Local\Programs\Python\Python36-32\Scripts\mintapi-script.py " + arguments;
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
