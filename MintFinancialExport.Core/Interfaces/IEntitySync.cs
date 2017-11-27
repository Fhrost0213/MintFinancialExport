using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MintFinancialExport.Core.Entities;

namespace MintFinancialExport.Core.Interfaces
{
    public interface IEntitySync
    {
        void SyncAccounts(ObservableCollection<MintAccount> accountlist);
        void SyncAccounts(ObservableCollection<MintAccount> accountlist, List<AccountHistory> manualAccountHistory);
        void SyncAccounts(ObservableCollection<MintAccount> accountlist, List<AccountHistory> manualAccountList, int? nextRunId, DateTime asOfDate);
        void SyncNetWorth(int? runId);
        void RefreshAccounts();
    }
}