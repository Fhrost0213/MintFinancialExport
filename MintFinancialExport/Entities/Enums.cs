using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintFinancialExport.Entities
{
    public class Enums
    {
        public enum AccountType
        {
            Cash,

            [Description("Traditional Retirement Account")]
            TradRetirement,

            [Description("Roth Retirement Account")]
            RothRetirement,

            [Description("Health Savings Account")]
            HSA,

            [Description("Taxable Account")]
            Taxable,

            [Description("Real Estate")]
            RealEstate,

            [Description("Physical Asset")]
            Physical,
            Automobiles,

            [Description("Credit Cards")]
            CreditCards,

            [Description("Student Loans")]
            StudentLoans,
            Mortgages
        }
    }
}
