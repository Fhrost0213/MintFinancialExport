using MintFinancialExport.Core;
using MintFinancialExport.Interfaces;
using MintFinancialExport.Views;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace MintFinancialExport.ViewModels
{
    class MintFinancialExportViewModel : BaseViewModel, IMintFinancialExportViewModel
    {

        #region "Private fields"
        MintApi _mintApi;
        private decimal? _netWorthAmount;
        private System.DateTime? _asOfDate;
        private ICommand _retrieveAccountsCommand;
        private ICommand _exportNetWorthCommand;
        private ICommand _accountMappingCommand;
        private ICommand _accountBrowserCommand;
        #endregion

        #region "Public commands"
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
        #endregion

        #region "Public properties"
        public decimal? NetWorthAmount
        {
            get { return _netWorthAmount; }
            set { _netWorthAmount = value; }
        }

        public System.DateTime? AsOfDate
        {
            get { return _asOfDate; }
            set { _asOfDate = value; }
        }

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

            EntitySync.SyncAccounts(_mintApi.GetAccounts(), manualAccountHistory);
        }

        private void ExportNetWorthCommandExecuted(object obj)
        {
            Export export = new Export();
            ExportObjects objects = new ExportObjects();

            export.ExportAccounts(objects.GetExportAccountList(), objects.GetExportAccountList(DataAccess.GetPreviousRunId(DataAccess.GetCurrentRunId())));
        } 

        public MintFinancialExportViewModel()
        {
            _mintApi = new MintApi();

            RetrieveAccountsCommand = new RelayCommand(RetrieveAccountsCommandExecuted);
            ExportNetWorthCommand = new RelayCommand(ExportNetWorthCommandExecuted);
            AccountMappingCommand = new RelayCommand(AccountMappingCommandExecuted);
            AccountBrowserCommand = new RelayCommand(AccountBrowserCommandExecuted);

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
