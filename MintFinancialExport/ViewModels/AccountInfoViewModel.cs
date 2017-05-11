using MintFinancialExport.Data;
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
        private string _password { get; set; }

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;

                User user = DataAccess.GetUserFromUserName(_userName);
                if (user == null)
                {
                    user = new User();
                    user.UserName = _userName;
                }
                
                user.LastUsedDate = DateTime.Now;
                DataAccess.SaveItem(user);

                OnPropertyChanged("UserName");
            }
        }

        public string Password
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
            var lastUsedUser = DataAccess.GetList<User>().OrderByDescending(d => d.LastUsedDate).FirstOrDefault();
            if (lastUsedUser != null)
            {
                UserName = lastUsedUser.UserName;
            }
            
            // Get Account Info from DB if it exists
        }
    }
}
