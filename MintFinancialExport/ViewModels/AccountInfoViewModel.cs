using MintFinancialExport.Core.Entities;
using MintFinancialExport.Core;
using System;
using System.Linq;
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

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand;
            }
            set
            {
                _saveCommand = value;
            }
        }

        private void SaveCommandExecuted(object obj)
        {
            AccountInfo.UserName = UserName;
            AccountInfo.Password = Password;
        }

        public AccountInfoViewModel()
        {
            SaveCommand = new RelayCommand(SaveCommandExecuted);

            var lastUsedUser = DataAccess.GetList<User>().OrderByDescending(d => d.LastUsedDate).FirstOrDefault();
            if (lastUsedUser != null)
            {
                UserName = lastUsedUser.UserName;
            }
        }
    }
}
