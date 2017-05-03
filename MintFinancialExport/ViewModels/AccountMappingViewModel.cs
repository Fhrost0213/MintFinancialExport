using MintFinancialExport.Entities;
using MintFinancialExport.Models;
using MintFinancialExport.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MintFinancialExport.ViewModels
{
    class AccountMappingViewModel : BaseViewModel
    {
        public ObservableCollection<Account> AccountList { get; set; }


        public AccountMappingViewModel()
        {
            RefreshAccountsCommand = new RelayCommand(RefreshAccountsCommandExecuted);
        }

        private Enums.AccountType _accountType;

        public Enums.AccountType AccountType
        {
            get { return _accountType; }
            set
            {
                if (_accountType != value)
                {
                    _accountType = value;
                    OnPropertyChanged(nameof(AccountType));
                }
            }
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

        private void RefreshAccountsCommandExecuted(object obj)
        {
            //AccountInfoView accountInfoView = new AccountInfoView();
            //accountInfoView.ShowDialog();
            //string userName = accountInfoView.txtUserName.Text;
            //string password = accountInfoView.txtPassword.Text;

            //MintFinancialExportModel mintFinancialExportModel = new MintFinancialExportModel();

            //AccountList = mintFinancialExportModel.GetAccounts(userName, password);
        }
    }
}
