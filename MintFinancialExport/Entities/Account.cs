using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintFinancialExport.Entities
{
    public class Account
    {
        public string LinkedAccountId { get; set; }
        public string AccountName { get; set; }
        public string AddAccountDate { get; set; }

        public string FiLoginDisplayName { get; set; }
        public string CcAggrStatus { get; set; }
        public string ExclusionType { get; set; }
        public string LinkedAccount { get; set; }
        public string IsHiddenFromPlanningTrends { get; set; }
        public string IsTerminal { get; set; }
        public string LinkCreationTime { get; set; }
        public string IsActive { get; set; }
        public string AccountStatus { get; set; }
        public string AccountSystemStatus { get; set; }
        public string LastUpdated { get; set; }
        public string FiLastUpdated { get; set; }
        public string YodleeAccountNumberLast4 { get; set; }
        public string IsError { get; set; }
        public string FiName { get; set; }
        public string IsAccountNotFound { get; set; }
        public string Klass { get; set; }
        public string[] PossibleLinkAccounts { get; set; }
        public string LastUpdatedInString { get; set; }
        public string AccountTypeInt { get; set; }
        public string Currency { get; set; }
        public string Id { get; set; }
        public string IsHostAccount { get; set; }
        public decimal? Value { get; set; }
        public string FiLoginId { get; set; }
        public string UsageType { get; set; }
        public string InterestRate { get; set; }
        public string AccountType { get; set; }
        public string CurrentBalance { get; set; }
        public string FiLoginStatus { get; set; }

        public string IsAccountClosedByMint { get; set; }
        public string UserName { get; set; }
        public string YodleeName { get; set; }
        public string CloseDate { get; set; }
        public string LinkStatus { get; set; }
        public string AccountId { get; set; }
        public string IsClosed { get; set; }
        public string FiLoginUIStatus { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string AddAccountDateInDate { get; set; }
        public string CloseDateInDate { get; set; }
        public string FiLastUpdatedInDate { get; set; }
        public string LastUpdatedInDate { get; set; }

    }
}
