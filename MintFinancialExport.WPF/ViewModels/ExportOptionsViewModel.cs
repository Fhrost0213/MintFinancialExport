using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using MintFinancialExport.Core;

namespace MintFinancialExport.WPF.ViewModels
{
    public class ExportOptionsViewModel : BaseViewModel
    {
        #region "Private Properties"
        private Visibility _compareVisibility;
        private bool _chkCompare;
        private List<AccountHistory> _accountHistoryList;
        private string _filePath;
        #endregion

        #region "Public Properties"
        public int SelectedExportRunId { get; set; }
        public int SelectedCompareRunId { get; set; }

        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                OnPropertyChanged(nameof(FilePath));
            }
        }


        public Visibility CompareVisibility
        {
            get { return _compareVisibility; }
            set
            {
                _compareVisibility = value;
                OnPropertyChanged(nameof(CompareVisibility));
            }
        }

        public bool ChkCompare
        {
            get { return _chkCompare; }
            set
            {
                _chkCompare = value;

                if (value) CompareVisibility = Visibility.Visible;
                else CompareVisibility = Visibility.Hidden;
            }
        }

        public List<AccountHistory> AccountHistoryList

        {
            get { return _accountHistoryList; }
            set
            {
                _accountHistoryList = value;
                OnPropertyChanged(nameof(AccountHistoryList));
            }
        }
        
        public ICommand ExportCommand { get; set; }

        public ICommand FileBrowserCommand { get; set; }

        #endregion

        #region "Constructors"
        public ExportOptionsViewModel()
        {
            AccountHistoryList = DataAccess.GetList<AccountHistory>()
                .GroupBy(r => r.RunId)
                .Select(x => x.First())
                .ToList();

            ExportCommand = new RelayCommand(ExportCommandExecuted, ExportCommandCanExecute);
            FileBrowserCommand = new RelayCommand(FileBrowserCommandExecuted);
            ChkCompare = false;
        }
        #endregion

        #region "Private Methods"
        private void FileBrowserCommandExecuted(object obj)
        {
            FilePath = GetSaveFilePath();
        }

        private bool ExportCommandCanExecute(object obj)
        {
            if (String.IsNullOrEmpty(FilePath)) return false;
                return true;
        }

        private void ExportCommandExecuted(object obj)
        {
            Export export = new Export();
            ExportObjects objects = new ExportObjects();

            Task task = new Task(() =>
            {
                export.ExportAccounts(objects.GetExportAccountList(SelectedExportRunId), FilePath, objects.GetExportAccountList(SelectedCompareRunId));
                MessageBox.Show("Export completed successfully!", "Export to Excel status:");
            });
            
            task.Start();
        }

        private string GetSaveFilePath()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = "NetWorthStatement_" + DateTime.Now.ToShortDateString().Replace("/", "_") + ".xlsx";
            dialog.Filter = "Excel (*.xlsx)|*.xlsx";

            dialog.ShowDialog();

            return dialog.FileName;
        }
        #endregion
    }
}
