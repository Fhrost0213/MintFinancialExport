namespace MintFinancialExport.ViewModels
{
    class ManualAccountViewModel : BaseViewModel
    {
        private string _accountName { get; set; }
        private decimal? _value { get; set; }

        public string AccountName
        {
            get
            {
                return _accountName;
            }
            set
            {
                _accountName = value;
                OnPropertyChanged("AccountName");
            }
        }

        public decimal? Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                OnPropertyChanged("Value");
            }
        }
    }
}
