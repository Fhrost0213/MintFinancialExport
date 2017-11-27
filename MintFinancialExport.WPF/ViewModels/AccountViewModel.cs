using MintFinancialExport.Core;
using System.Collections.Generic;
using System.Windows.Input;
using MintFinancialExport.Core.Interfaces;

namespace MintFinancialExport.WPF.ViewModels
{
    class AccountViewModel : BaseViewModel
    {
        private IDataAccess _dataAccess;

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
            _dataAccess = ServiceLocator.GetInstance<IDataAccess>();

            SaveCommand = new RelayCommand(SaveCommandExecuted);
            RefreshAccountsCommand = new RelayCommand(RefreshAccountsCommandExecuted);
            AccountList = _dataAccess.GetList<Account>();
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
            _dataAccess.SaveList(AccountList);
        }

        private void RefreshAccountsCommandExecuted(object obj)
        {
            var entitySync = ServiceLocator.GetInstance<IEntitySync>();
            entitySync.RefreshAccounts();

            AccountList = _dataAccess.GetList<Account>();
        }
    }
}
