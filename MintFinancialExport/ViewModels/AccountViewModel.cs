using MintFinancialExport.Data;
using System.Collections.Generic;
using System.Windows.Input;

namespace MintFinancialExport.ViewModels
{
    class AccountViewModel : BaseViewModel
    {
        private List<Account> _accountList;
        public List<Account> AccountList
        {
            get { return _accountList; }
            set
            {
                _accountList = value;
                OnPropertyChanged("AccountMappingList");
            }
        }

        public AccountViewModel()
        {
            SaveCommand = new RelayCommand(SaveCommandExecuted);
            RefreshAccountsCommand = new RelayCommand(RefreshAccountsCommandExecuted);
            AccountList = DataAccess.GetList<Account>();
        }


        private ICommand _refreshAccountsCommand;
        public ICommand RefreshAccountsCommand
        {
            get
            {
                return _refreshAccountsCommand;
            }
            set
            {
                _refreshAccountsCommand = value;
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
            DataAccess.SaveList(AccountList);
        }

        private void RefreshAccountsCommandExecuted(object obj)
        {
            EntitySync.RefreshAccounts();

            AccountList = DataAccess.GetList<Account>();
        }
    }
}
