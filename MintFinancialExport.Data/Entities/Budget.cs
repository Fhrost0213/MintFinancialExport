using System.Collections.Generic;

namespace MintFinancialExport.Core.Entities
{
    class Budget
    {
        public string[] Income { get; set; }
        public List<Spend> Spend { get; set; }
    }
}
