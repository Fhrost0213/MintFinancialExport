using MintFinancialExport.Entities;
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
        public List<AccountMapping> AccountMappingList
        {
            get { return _accountMappingList; }
            set
            {
                _accountMappingList = value;
                OnPropertyChanged("AccountMappingList");
            }
        }

        private List<AccountType> _accountTypesList;
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
            AccountMappingList = DataAccess.GetAccountMappings();
            AccountTypesList = DataAccess.GetAccountTypes();
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
