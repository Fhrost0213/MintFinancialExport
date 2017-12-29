using System;
using MintFinancialExport.Core;
using MintFinancialExport.WPF.Interfaces;
using MintFinancialExport.WPF.Views;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MintFinancialExport.Core.Entities;
using MintFinancialExport.Core.Interfaces;

namespace MintFinancialExport.WPF.ViewModels
{
    class MintFinancialExportViewModel : BaseViewModel, IMintFinancialExportViewModel
    {

        #region "Private fields"
        IMintApi _mintApi;
        private double _currentProgress;
        private IDataAccess _dataAccess;
        private decimal? _netWorthAmount;
        private DateTime? _asOfDate;
        private bool _progressVisibility;
        private readonly BackgroundWorker _progressBarWorker;
        #endregion

        #region "Public commands"
        public ICommand RetrieveAccountsCommand { get; set; }

        public ICommand ExportNetWorthCommand { get; set; }

        public ICommand OptionsCommand { get; set; }

        public ICommand AccountMappingCommand { get; set; }

        public ICommand AccountBrowserCommand { get; set; }

        #endregion

        #region "Public properties"

        public decimal? NetWorthAmount
        {
            get
            {
                return _netWorthAmount;
            }
            set
            {
                _netWorthAmount = value;
                OnPropertyChanged("NetWorthAmount");
            }
        }

        public double CurrentProgress
        {
            get { return _currentProgress; }
            private set
            {
                if (_currentProgress != value)
                {
                    _currentProgress = value;
                    OnPropertyChanged("CurrentProgress");
                }
            }
        }

        public bool ProgressVisibility
        {
            get { return _progressVisibility; }
            private set
            {
                _progressVisibility = value;
                OnPropertyChanged("ProgressVisibility");
            }
        }

        public DateTime? AsOfDate
        {
            get
            {
                return _asOfDate;
            }
            set
            {
                _asOfDate = value;
                OnPropertyChanged("AsOfDate");
            }
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
            var accountInfoHandler = ServiceLocator.GetInstance<IAccountInfoHandler>();
            accountInfoHandler.Show();

            // Call new method
            _progressBarWorker.RunWorkerAsync();
        }

        private async void CallSyncAccountsAsync(List<AccountHistory> manualAccountHistory)
        {
            var entitySync = ServiceLocator.GetInstance<IEntitySync>();

            Task task = new Task(() =>
            {
                //EntitySync.SyncAccounts(_mintApi.GetAccountsExtended(), manualAccountHistory);
                entitySync.SyncAccounts(_mintApi.GetAccounts(), manualAccountHistory);
            });

            task.Start();
            await task;
        }

        private void CallSyncAccounts(List<AccountHistory> manualAccountHistory)
        {
            var entitySync = ServiceLocator.GetInstance<IEntitySync>();

            entitySync.SyncAccounts(_mintApi.GetAccounts(), manualAccountHistory);
        }

        private void ExportNetWorthCommandExecuted(object obj)
        {
            ExportOptionsView view = new ExportOptionsView();
            //ExportOptionsViewModel viewModel = new ExportOptionsViewModel();
            //view.DataContext = viewModel;
            view.Show();
        }

        private void OptionsCommandExecuted(object obj)
        {
            OptionsViewModel model = new OptionsViewModel();
            OptionsView view = new OptionsView {DataContext = model};

            view.ShowDialog();
        }

        public MintFinancialExportViewModel()
        {
            Bootstrapper.ConfigureStructureMap();

            _dataAccess = ServiceLocator.GetInstance<IDataAccess>();

            _mintApi = ServiceLocator.GetInstance<IMintApi>();

            _progressBarWorker = new BackgroundWorker();
            _progressBarWorker.DoWork += SyncAccountsWork;
            _progressBarWorker.ProgressChanged += ProgressChanged;

            RetrieveAccountsCommand = new RelayCommand(RetrieveAccountsCommandExecuted);
            ExportNetWorthCommand = new RelayCommand(ExportNetWorthCommandExecuted);
            AccountMappingCommand = new RelayCommand(AccountMappingCommandExecuted);
            AccountBrowserCommand = new RelayCommand(AccountBrowserCommandExecuted);
            OptionsCommand = new RelayCommand(OptionsCommandExecuted);

            RefreshAccountInfo();
        }

        private void SyncAccountsWork(object sender, DoWorkEventArgs e)
        {
            ProgressVisibility = true;

            ProgressChanged(null, new ProgressChangedEventArgs(10, e));

            // TODO: Does this block of code need to be pulled out and refactored?
            // Prompt for manual values
            var runId = _dataAccess.GetNextRunId();

            var accounts = _dataAccess.GetList<Account>();
            var manualAccounts = accounts.FindAll(m => m.IsManual == true);

            List<AccountHistory> manualAccountHistory = new List<AccountHistory>();

            foreach (var account in manualAccounts)
            {
                AccountHistory accountHistory = new AccountHistory();

                accountHistory.AccountId = account.ObjectId;
                accountHistory.AsOfDate = DateTime.Now;
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

                    var previousHistory = _dataAccess.GetList<AccountHistory>().Where(a => a.AccountId == account.ObjectId).OrderByDescending(r => r.RunId).FirstOrDefault();

                    if (previousHistory != null) model.Value = previousHistory.Amount;

                    view.ShowDialog();

                    accountHistory.Amount = model.Value;

                }

                manualAccountHistory.Add(accountHistory);
            }

            ProgressChanged(null, new ProgressChangedEventArgs(20, e));
            CallSyncAccounts(manualAccountHistory);
            ProgressChanged(null, new ProgressChangedEventArgs(90, e));

            RefreshAccountInfo();
            ProgressChanged(null, new ProgressChangedEventArgs(100, e));

            ProgressVisibility = false;
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CurrentProgress = e.ProgressPercentage;
        }

        private void RefreshAccountInfo()
        {
            var runId = _dataAccess.GetCurrentRunId();

            if (AccountList == null)
                AccountList = _dataAccess.GetList<AccountHistory>().Where(r => r.RunId == runId).ToList();

            AccountList.Clear();
            
            AccountList.AddRange(_dataAccess.GetList<AccountHistory>().Where(r => r.RunId == runId).ToList());

            var netWorthInfo = _dataAccess.GetList<NetWorthHistory>().FirstOrDefault(r => r.RunId == runId);

            if (netWorthInfo != null)
            {
                NetWorthAmount = netWorthInfo.NetWorthAmount;
                AsOfDate = netWorthInfo.AsOfDate;
            }
        }
    }
}
