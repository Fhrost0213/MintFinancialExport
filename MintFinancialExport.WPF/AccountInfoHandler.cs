using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using MintFinancialExport.Core.Entities;
using MintFinancialExport.WPF.Views;

namespace MintFinancialExport.WPF
{
    public class AccountInfoHandler
    {
        public void Show()
        {
            AccountInfoView infoView = new AccountInfoView();
            infoView.ShowDialog();
            // This isn't ideal but MVVM makes passwords a bit more difficult
            AccountInfo.Password = ConvertToUnsecureString(infoView.Password);
        }

        private string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
            {
                return string.Empty;
            }

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
