using MintFinancialExport.Core;
using MintFinancialExport.Core.Entities;
using MintFinancialExport.Core.Interfaces;

namespace MintFinancialExport.WPF.ViewModels
{
    class OptionsViewModel : BaseViewModel
    {
        private IDataAccess _dataAccess;

        public OptionsViewModel()
        {
            _dataAccess = ServiceLocator.GetInstance<IDataAccess>();

            PythonFolderLocation = _dataAccess.GetOption(Enums.Options.PythonFolderLocation.ToString());
        }

        private string _pythonFolderLocation;

        public string PythonFolderLocation
        {
            get
            {
                return _pythonFolderLocation; 
                
            }
            set
            {
                if (value != null)
                {
                    _pythonFolderLocation = value;

                    _dataAccess.SaveOption(Enums.Options.PythonFolderLocation.ToString(), value);

                    OnPropertyChanged("PythonFolderLocation");
                }
            }
        }
    }
}
