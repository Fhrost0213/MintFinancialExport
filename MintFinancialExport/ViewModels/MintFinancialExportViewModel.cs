using MintFinancialExport.Data;
using MintFinancialExport.Interfaces;
using MintFinancialExport.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MintFinancialExport.ViewModels
{
    class MintFinancialExportViewModel : BaseViewModel, IMintFinancialExportViewModel
    {
        MintApi _mintApi;

        public ObservableCollection<Core.Entities.MintAccount> AccountList { get; set; }

        public MintFinancialExportViewModel()
        {
            _mintApi = new MintApi();

            RetrieveAccountsCommand = new RelayCommand(RetrieveAccountsCommandExecuted);
            ExportNetWorthCommand = new RelayCommand(ExportNetWorthCommandExecuted);
            AccountMappingCommand = new RelayCommand(AccountMappingCommandExecuted);
            AccountBrowserCommand = new RelayCommand(AccountBrowserCommandExecuted);
        }

        private ICommand _retrieveAccountsCommand;
        public ICommand RetrieveAccountsCommand
        {
            get
            {
                return _retrieveAccountsCommand;
            }
            set
            {
                _retrieveAccountsCommand = value;
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

        private void RetrieveAccountsCommandExecuted(object obj)
        {
            AccountInfoView infoView = new AccountInfoView();
            infoView.ShowDialog();

            //Export export = new Export();

            // TODO: Fixing password to be secure. Store in DB encrypted to avoid typing it in

            AccountList = _mintApi.GetAccounts();


            // Prompt for manual values
            var accounts = DataAccess.GetList<Account>();
            var manualAccounts = accounts.FindAll(m => m.IsManual == true);

            List<AccountHistory> manualAccountHistory = new List<AccountHistory>();

            foreach (var account in manualAccounts)
            {
                ManualAccountView view = new ManualAccountView();
                ManualAccountViewModel model = new ManualAccountViewModel();
                view.DataContext = model;
                model.AccountName = account.AccountName;

                var previousHistory = DataAccess.GetList<AccountHistory>().Where(a => a.AccountId == account.ObjectId).OrderByDescending(r => r.RunId).FirstOrDefault();

                if (previousHistory != null) model.Value = previousHistory.Amount;

                view.ShowDialog();

                AccountHistory accountHistory = new AccountHistory();
                accountHistory.Account = account;
                accountHistory.AccountId = account.ObjectId;
                accountHistory.Amount = model.Value;
                accountHistory.AsOfDate = System.DateTime.Now;
                accountHistory.RunId = DataAccess.GetNextRunId();

                manualAccountHistory.Add(accountHistory);
            }

            EntitySync.SyncAccounts(AccountList, manualAccountHistory);

            //export.ExportToExcel(AccountList, _physicalAssetsAmount, _mortgageAmount);

            //GetBudget();

            //GetNetWorth();

            //GetTransactions();

            
            //db.MspAccountInsert(2, "EntityTest");
        }

        private void ExportNetWorthCommandExecuted(object obj)
        {
            Export export = new Export();
            ExportObjects objects = new ExportObjects();

            export.ExportAccounts(objects.GetExportAccountList());
        }
    }
}
