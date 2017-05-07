namespace MintFinancialExport.Core.Entities
{
    public class ExportAccount
    {
        private int _accountTypeID;

        public int AccountTypeID
        {
            get { return _accountTypeID; }
            set { _accountTypeID = value; }
        }

        private string _accountTypeName;

        public string AccountTypeName
        {
            get { return _accountTypeName; }
            set { _accountTypeName = value; }
        }

        private decimal? _value;

        public decimal? Value
        {
            get { return _value; }
            set { _value = value; }
        }

        private bool? _isAsset;
        public bool? IsAsset
        {
            get { return _isAsset; }
            set { _isAsset = value; }
        }
    }
}
