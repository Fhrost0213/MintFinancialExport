using MintFinancialExport.Core.Entities;
using MintFinancialExport.Models;
using MintFinancialExport.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MintFinancialExport.ViewModels
{
    class AccountMappingViewModel : BaseViewModel
    {
        private List<AccountMapping> _accountMappingList;
        private List<Account> _accountList;
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

        public List<Account> AccountList
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

        private void LoadAccountMappings()
        {
            MyDbContext db = new MyDbContext();
            AccountMappingList = db.AccountMappings.ToList();
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
           // DataAccess.SaveAccountMappings(AccountMappingList);
        }

        
    }
}
