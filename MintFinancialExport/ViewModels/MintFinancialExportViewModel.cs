using MintFinancialExport.Data;
using MintFinancialExport.Interfaces;
using MintFinancialExport.Models;
using MintFinancialExport.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MintFinancialExport.ViewModels
{
    class MintFinancialExportViewModel : BaseViewModel, IMintFinancialExportViewModel
    {
        MintFinancialExportModel _mintFinancialExportModel;

        public ObservableCollection<Core.Entities.MintAccount> AccountList { get; set; }

        public MintFinancialExportViewModel()
        {
            _mintFinancialExportModel = new MintFinancialExportModel();

            ClickCommand = new RelayCommand(ClickCommandExecuted);
            ExportNetWorthCommand = new RelayCommand(ExportNetWorthCommandExecuted);
            AccountMappingCommand = new RelayCommand(AccountMappingCommandExecuted);
            AccountBrowserCommand = new RelayCommand(AccountBrowserCommandExecuted);
        }

        private ICommand _clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                return _clickCommand;
            }
            set
            {
                _clickCommand = value;
            }
        }

        private ICommand _exportNetWorthCommand;
        public ICommand ExportNetWorthCommand
        {
            get
            {
                return _exportNetWorthCommand;
            }
            set
            {
                _exportNetWorthCommand = value;
            }
        }

        private ICommand _accountMappingCommand;
        public ICommand AccountMappingCommand
        {
            get
            {
                return _accountMappingCommand;
            }
            set
            {
                _accountMappingCommand = value;
            }
        }

        private ICommand _accountBrowserCommand;
        public ICommand AccountBrowserCommand
        {
            get
            {
                return _accountBrowserCommand;
            }
            set
            {
                _accountBrowserCommand = value;
            }
        }

        private void AccountBrowserCommandExecuted(object obj)
        {
            AccountView accountView = new AccountView();
            accountView.ShowDialog();

        }

        private void AccountMappingCommandExecuted(object obj)
        {
            AccountMappingView accountMappingView = new AccountMappingView();
            accountMappingView.ShowDialog();

        }

        private void ClickCommandExecuted(object obj)
        {
            AccountInfoView accountInfoView = new AccountInfoView();
            accountInfoView.ShowDialog();
            string userName = accountInfoView.txtUserName.Text;
            string password = accountInfoView.txtPassword.Text;

            //Export export = new Export();

            if (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(password))
            {
                AccountList = _mintFinancialExportModel.GetAccounts(userName, password);
            }

            EntitySync.SyncAccounts(AccountList);



            //export.ExportToExcel(AccountList, _physicalAssetsAmount, _mortgageAmount);

            //GetBudget();

            //GetNetWorth();

            //GetTransactions();

            
            //db.MspAccountInsert(2, "EntityTest");
        }

        private void ExportNetWorthCommandExecuted(object obj)
        {
            Export export = new Export();
            export.ExportAccounts();
        }
    }
}
