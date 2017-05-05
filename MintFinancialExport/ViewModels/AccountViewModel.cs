using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            AccountList = DataAccess.GetAccounts();
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
            //AccountInfoView accountInfoView = new AccountInfoView();
            //accountInfoView.ShowDialog();
            //string userName = accountInfoView.txtUserName.Text;
            //string password = accountInfoView.txtPassword.Text;

            //MintFinancialExportModel mintFinancialExportModel = new MintFinancialExportModel();

            //var accountList = mintFinancialExportModel.GetAccounts(userName, password);

            //foreach (Entities.Account account in accountList)
            //{
                //Entities.AccountMapping mapping = new Entities.AccountMapping();
                //mapping.AccountName = account.Name;
                //mapping.AccountType = Enums.AccountType.Taxable;

                //AccountMappingList.Add(mapping);
            //}
        }
    }
}
