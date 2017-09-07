using System;
using MintFinancialExport.Core;
using MintFinancialExport.WPF.Interfaces;
using MintFinancialExport.WPF.Views;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MintFinancialExport.WPF.ViewModels
{
    class MintFinancialExportViewModel : BaseViewModel, IMintFinancialExportViewModel
    {

        #region "Private fields"
        MintApi _mintApi;

        #endregion

        #region "Public commands"
        public ICommand RetrieveAccountsCommand { get; set; }

        public ICommand ExportNetWorthCommand { get; set; }

        public ICommand OptionsCommand { get; set; }

        public ICommand AccountMappingCommand { get; set; }

        public ICommand AccountBrowserCommand { get; set; }

        #endregion

        #region "Public properties"
        public decimal? NetWorthAmount { get; set; }

        public System.DateTime? AsOfDate { get; set; }

        public List<AccountHistory> AccountList { get; set; }
        #endregion

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

            // TODO: Fixing password to be secure. Store in DB encrypted to avoid typing it in

            //AccountList = _mintApi.GetAccounts();

            // TODO: Does this block of code need to be pulled out and refactored?
            // Prompt for manual values
            var runId = DataAccess.GetNextRunId();

            var accounts = DataAccess.GetList<Account>();
            var manualAccounts = accounts.FindAll(m => m.IsManual == true);

            List<AccountHistory> manualAccountHistory = new List<AccountHistory>();

            foreach (var account in manualAccounts)
            {
                AccountHistory accountHistory = new AccountHistory();

                accountHistory.AccountId = account.ObjectId;
                accountHistory.AsOfDate = System.DateTime.Now;
                accountHistory.RunId = runId;

                var type = account.AccountMappings.FirstOrDefault();
                if (type.AccountType.AccountTypeName == "Physical")
                {
                    PreciousMetalsView preciousMetalsView = new PreciousMetalsView();
                    PreciousMetalsViewModel preciousMetalsViewModel = new PreciousMetalsViewModel(runId);
                    preciousMetalsView.DataContext = preciousMetalsViewModel;
                    preciousMetalsView.ShowDialog();

                    accountHistory.Amount = preciousMetalsViewModel.GetTotals();
                }
                else
                {
                    ManualAccountView view = new ManualAccountView();
                    ManualAccountViewModel model = new ManualAccountViewModel();
                    view.DataContext = model;
                    model.AccountName = account.AccountName;

                    var previousHistory = DataAccess.GetList<AccountHistory>().Where(a => a.AccountId == account.ObjectId).OrderByDescending(r => r.RunId).FirstOrDefault();

                    if (previousHistory != null) model.Value = previousHistory.Amount;

                    view.ShowDialog();
                    
                    accountHistory.Amount = model.Value;

                }

                manualAccountHistory.Add(accountHistory);
            }

            CallSyncAccountsAsync(manualAccountHistory);
        }

        private async void CallSyncAccountsAsync(List<AccountHistory> manualAccountHistory)
        {
            Task task = new Task(() =>
            {
                EntitySync.SyncAccounts(_mintApi.GetAccounts(), manualAccountHistory);
            });

            task.Start();
            await task;
        }

        private void ExportNetWorthCommandExecuted(object obj)
        {
            Export export = new Export();
            ExportObjects objects = new ExportObjects();

            export.ExportAccounts(objects.GetExportAccountList(), objects.GetExportAccountList(DataAccess.GetPreviousRunId(DataAccess.GetCurrentRunId())));
        }

        private void OptionsCommandExecuted(object obj)
        {
            OptionsViewModel model = new OptionsViewModel();
            OptionsView view = new OptionsView {DataContext = model};

            view.ShowDialog();
        }

        public MintFinancialExportViewModel()
        {
            _mintApi = new MintApi();

            RetrieveAccountsCommand = new RelayCommand(RetrieveAccountsCommandExecuted);
            ExportNetWorthCommand = new RelayCommand(ExportNetWorthCommandExecuted);
            AccountMappingCommand = new RelayCommand(AccountMappingCommandExecuted);
            AccountBrowserCommand = new RelayCommand(AccountBrowserCommandExecuted);
            OptionsCommand = new RelayCommand(OptionsCommandExecuted);

            RefreshAccountInfo();
        }

        private void RefreshAccountInfo()
        {
            // Get latest account history
            var runId = DataAccess.GetCurrentRunId();
            AccountList = DataAccess.GetList<AccountHistory>().Where(r => r.RunId == runId).ToList();

            var netWorthInfo = DataAccess.GetList<NetWorthHistory>().Where(r => r.RunId == runId);
            NetWorthAmount = netWorthInfo.FirstOrDefault().NetWorthAmount;
            AsOfDate = netWorthInfo.FirstOrDefault().AsOfDate;
        }

        
    }
}
