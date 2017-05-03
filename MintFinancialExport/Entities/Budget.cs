using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintFinancialExport.Entities
{
    class Budget
    {
        public string[] Income { get; set; }
        public List<Spend> Spend { get; set; }
    }

    public class Spend
    {
        public string St { get; set; }
        public string Ramt { get; set; }
        public string IsIncome { get; set; }
        public string IsTransfer { get; set; }
        public string IsExpense { get; set; }
        public string Amt { get; set; }
        public string Pid { get; set; }
        public string Type { get; set; }
        public string Bgt { get; set; }
        public string Rbal { get; set; }
        public string Ex { get; set; }
        public string Cat { get; set; }
        public string Id { get; set; }
        public string CatTypeFilter { get; set; }
    }
}
