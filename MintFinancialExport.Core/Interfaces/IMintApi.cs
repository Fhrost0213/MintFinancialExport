using System.Collections.ObjectModel;
using MintFinancialExport.Core.Entities;

namespace MintFinancialExport.Core.Interfaces
{
    public interface IMintApi
    {
        void GetTransactions();
        void GetNetWorth();
        ObservableCollection<MintAccount> GetAccounts();
        ObservableCollection<MintAccount> GetAccounts(string userName, string password);
        void GetBudget();
        ObservableCollection<MintAccount> GetAccountsExtended();
        string GetMintInfo(string arguments);
    }
}