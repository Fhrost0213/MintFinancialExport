using MintFinancialExport.Core;
using System.Collections.Generic;
using System.Windows.Input;

namespace MintFinancialExport.WPF.ViewModels
{
    class AccountMappingViewModel : BaseViewModel
    {
        private List<AccountMapping> _accountMappingList;
        private List<Account> _accountList;
        private List<AccountType> _accountTypesList;

        private IDataAccess _dataAccess;

        public AccountMappingViewModel()
        {
            _dataAccess = ServiceLocator.GetInstance<IDataAccess>();

            AccountList = _dataAccess.GetList<Account>();
            AccountMappingList = _dataAccess.GetList<AccountMapping>();
            AccountTypesList = _dataAccess.GetList<AccountType>();
            SaveCommand = new RelayCommand(SaveCommandExecuted);
        }

        public List<AccountMapping> AccountMappingList
        {
            get { return _accountMappingList; }
            set
            {
                _accountMappingList = value;
                OnPropertyChanged("AccountMappingList");
            }
        }

        public List<Core.Account> AccountList
        {
            get { return _accountList; }
            set
            {
                _accountList = value;
                OnPropertyChanged("AccountList");
            }
        }

        public List<AccountType> AccountTypesList
        {
            get { return _accountTypesList; }
            set
            {
                _accountTypesList = value;
                OnPropertyChanged("AccountTypesList");
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
            _dataAccess.SaveList(AccountMappingList);
        }
    }
}
