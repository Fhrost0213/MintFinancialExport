using MintFinancialExport.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MintFinancialExport.ViewModels
{
    class AccountInfoViewModel : BaseViewModel
    {
        private string _userName { get; set; }
        private System.Security.SecureString _password { get; set; }

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                OnPropertyChanged("UserName");
            }
        }

        public System.Security.SecureString Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        public AccountInfoViewModel()
        {
            // Get Account Info from DB if it exists
        }
    }
}
