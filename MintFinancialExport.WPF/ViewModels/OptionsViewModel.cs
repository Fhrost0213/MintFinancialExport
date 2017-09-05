using MintFinancialExport.Core;
using MintFinancialExport.Core.Entities;

namespace MintFinancialExport.WPF.ViewModels
{
    class OptionsViewModel : BaseViewModel
    {
        public OptionsViewModel()
        {
            PythonFolderLocation = DataAccess.GetOption(Enums.Options.PythonFolderLocation.ToString());
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

                    DataAccess.SaveOption(Enums.Options.PythonFolderLocation.ToString(), value);

                    OnPropertyChanged("PythonFolderLocation");
                }
            }
        }
    }
}
