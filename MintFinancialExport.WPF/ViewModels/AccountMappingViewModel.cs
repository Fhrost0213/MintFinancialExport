using MintFinancialExport.Core;
using System.Collections.Generic;
using System.Windows.Input;

namespace MintFinancialExport.WPF.ViewModels
{
    class AccountMappingViewModel : BaseViewModel
    {
        private List<AccountMapping> _accountMappingList;
        private List<Core.Account> _accountList;
        private List<AccountType> _accountTypesList;

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

        public AccountMappingViewModel()
        { 
            AccountList = DataAccess.GetList<Account>();
            AccountMappingList = DataAccess.GetList<AccountMapping>();
            AccountTypesList = DataAccess.GetList<AccountType>();
            SaveCommand = new RelayCommand(SaveCommandExecuted);
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
            DataAccess.SaveList(AccountMappingList);
        }
    }
}
