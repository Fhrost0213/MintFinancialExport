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
        public ObservableCollection<AccountMapping> AccountMappingList { get; set; }


        public AccountMappingViewModel()
        {
            AccountMappingList = new ObservableCollection<AccountMapping>();
            RefreshAccountsCommand = new RelayCommand(RefreshAccountsCommandExecuted);
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
            AccountInfoView accountInfoView = new AccountInfoView();
            accountInfoView.ShowDialog();
            string userName = accountInfoView.txtUserName.Text;
            string password = accountInfoView.txtPassword.Text;

            MintFinancialExportModel mintFinancialExportModel = new MintFinancialExportModel();

            var accountList = mintFinancialExportModel.GetAccounts(userName, password);

            foreach (Account account in accountList)
            {
                AccountMapping mapping = new AccountMapping();
                mapping.AccountName = account.Name;
                mapping.AccountType = Enums.AccountType.Taxable;

                AccountMappingList.Add(mapping);
            }
        }
    }
}
