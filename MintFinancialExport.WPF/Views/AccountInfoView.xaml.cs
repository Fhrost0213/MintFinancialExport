using MahApps.Metro.Controls;

namespace MintFinancialExport.WPF.Views
{
    /// <summary>
    /// Interaction logic for AccountInfoView.xaml
    /// </summary>
    public partial class AccountInfoView : MetroWindow
    {
        public AccountInfoView()
        {
            InitializeComponent();
        }

        public System.Security.SecureString Password
        {
            get { return txtPassword.SecurePassword; }
        }
    }
}
