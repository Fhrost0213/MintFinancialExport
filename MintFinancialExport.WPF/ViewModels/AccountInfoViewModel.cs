using MintFinancialExport.Core.Entities;
using MintFinancialExport.Core;
using System;
using System.Linq;
using System.Windows.Input;
using MintFinancialExport.Core.Interfaces;

namespace MintFinancialExport.WPF.ViewModels
{
    class AccountInfoViewModel : BaseViewModel
    {
        private string _userName { get; set; }
        private System.Security.SecureString _password { get; set; }

        private IDataAccess _dataAccess;

        public AccountInfoViewModel()
        {
            _dataAccess = ServiceLocator.GetInstance<IDataAccess>();

            SaveCommand = new RelayCommand(SaveCommandExecuted);

            var lastUsedUser = _dataAccess.GetList<User>().OrderByDescending(d => d.LastUsedDate).FirstOrDefault();
            if (lastUsedUser != null)
            {
                UserName = lastUsedUser.UserName;
            }
        }

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;

                User user = _dataAccess.GetUserFromUserName(_userName);
                if (user == null)
                {
                    user = new User();
                    user.UserName = _userName;
                }
                
                user.LastUsedDate = DateTime.Now;
                _dataAccess.SaveItem(user);

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
            //AccountInfo.Password = Password;
        }
    }
}
