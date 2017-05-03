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
        public void GetTransactions()
        {
            //var data = GetMintInfo("--extended-transactions --include-investment");
        }

        public void GetNetWorth()
        {
            //var data = GetMintInfo("--net-worth");
        }

        public ObservableCollection<Account> GetAccounts(string userName, string password)
        {
            //string session = "";
            //string guid = "";

            //ChromeCookieReader wb = new ChromeCookieReader();
            //var cookies = wb.ReadCookies("mint.intuit.com");
            //var test = wb.ReadCookies("pf.intuit.com");

            //foreach (var cookie in cookies)
            //{
            //    if (cookie.Item1 == "MINTJSESSIONID") session = cookie.Item2;
            //    if (cookie.Item1 == "userguid") guid = cookie.Item2;
  
            //}

            var data = GetMintInfo("--accounts", userName, password);

            return JsonConvert.DeserializeObject<ObservableCollection<Account>>(data);
        }

        public void GetBudget()
        {
            //var data = GetMintInfo("--budgets");

            //var test = JsonConvert.DeserializeObject<Budget>(data);
        }

        public string GetMintInfo(string info, string userName, string password)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = @"C:\Users\Bryan\AppData\Local\Programs\Python\Python36-32\python.exe";
            startInfo.WorkingDirectory = "C:\\windows\\system32";
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;

            startInfo.Arguments = @"C:\Users\Bryan\AppData\Local\Programs\Python\Python36-32\Scripts\mintapi-script.py " + info + " " + userName + " " + password;
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
