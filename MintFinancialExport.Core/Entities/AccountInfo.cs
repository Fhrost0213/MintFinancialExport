using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintFinancialExport.Core.Entities
{
    public static class AccountInfo
    {

        private static string _userName;
        private static string _password;

        public static string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public static string Password
        {
            get { return _password; }
            set { _password = value; }
        }

    }
}
