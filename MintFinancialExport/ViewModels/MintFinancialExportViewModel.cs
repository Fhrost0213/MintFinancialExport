using MintFinancialExport.Entities;
using MintFinancialExport.Interfaces;
using MintFinancialExport.Models;
using MintFinancialExport.Views;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace MintFinancialExport.ViewModels
{
    class MintFinancialExportViewModel : BaseViewModel, IMintFinancialExportViewModel
    {
        MintFinancialExportModel _mintFinancialExportModel;

        public ObservableCollection<Entities.Account> AccountList { get; set; }

        private decimal? _mortgageAmount { get; set; }
        private decimal? _physicalAssetsAmount { get; set; }

        public decimal? PhysicalAssetsAmount
        {
            get
            {
                return _physicalAssetsAmount;
            }
            set
            {
                _physicalAssetsAmount = value;
                OnPropertyChanged("PhysicalAssetsAmount");
            }
        }
        public decimal? MortgageAmount
        {
            get
            {
                return _mortgageAmount;
            }
            set
            {
                _mortgageAmount = value;
                OnPropertyChanged("MortgageAmount");
            }
        }

        public MintFinancialExportViewModel()
        {
            _mintFinancialExportModel = new MintFinancialExportModel();

            ClickCommand = new RelayCommand(ClickCommandExecuted);
            AccountMappingCommand = new RelayCommand(AccountMappingCommandExecuted);
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

            DataAccess.SyncAccounts(AccountList);



            //export.ExportToExcel(AccountList, _physicalAssetsAmount, _mortgageAmount);

            //GetBudget();

            //GetNetWorth();

            //GetTransactions();

            
            //db.MspAccountInsert(2, "EntityTest");
        }
    }
}
