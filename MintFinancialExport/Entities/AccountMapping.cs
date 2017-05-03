using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MintFinancialExport.Entities.Enums;

namespace MintFinancialExport.Entities
{
    class AccountMapping
    {
        private string _accountName;

        public string AccountName
        {
            get { return _accountName; }
            set { _accountName = value; }
        }

        private AccountType _accountType;

        public AccountType AccountType
        {
            get { return _accountType; }
            set { _accountType = value; }
        }

    }
}
