using MintFinancialExport.Core;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MintFinancialExport.Tests.Core
{
    [TestFixture]
    public class DataAccess
    {
        [Test]
        public void GetListOfAccount()
        {
            var list = MintFinancialExport.Core.DataAccess.GetList<Account>();
            Assert.IsInstanceOf<List<Account>>(list);
        }

        [Test]
        public void GetListOfAccountHistory()
        {
            var list = MintFinancialExport.Core.DataAccess.GetList<AccountHistory>();
            Assert.IsInstanceOf<List<AccountHistory>>(list);
        }

        [Test]
        public void GetListOfAccountMapping()
        {
            var list = MintFinancialExport.Core.DataAccess.GetList<AccountMapping>();
            Assert.IsInstanceOf<List<AccountMapping>>(list);
        }

        [Test]
        public void GetListOfAccountType()
        {
            var list = MintFinancialExport.Core.DataAccess.GetList<AccountType>();
            Assert.IsInstanceOf<List<AccountType>>(list);
        }

        [Test]
        public void SaveAndDeleteListOfAccount()
        {
            Account account = new Account();
            string name = "NUnit Tests - SaveListOfAccount";
            account.AccountName = name;

            MintFinancialExport.Core.DataAccess.SaveItem(account);

            var savedItem = MintFinancialExport.Core.DataAccess.GetList<Account>().Where(n => n.AccountName == name).First();

            Assert.That(savedItem != null);

            MintFinancialExport.Core.DataAccess.DeleteItem<Account>(savedItem.ObjectId);

            Assert.That(MintFinancialExport.Core.DataAccess.DoesItemExist<Account>(savedItem.ObjectId) == false);
        }

        [Test]
        public void DoesAccountExist()
        {
            bool added = false;

            var item = MintFinancialExport.Core.DataAccess.GetList<Account>().FirstOrDefault();

            if (item == null)
            {
                Account account = new Account();
                account.AccountName = "NUnit Tests - DoesAccountExist";

                MintFinancialExport.Core.DataAccess.SaveItem(account);
                added = true;
            }

            item = MintFinancialExport.Core.DataAccess.GetList<Account>().FirstOrDefault();

            bool isExists = MintFinancialExport.Core.DataAccess.DoesItemExist<Account>(item.ObjectId);
            Assert.That(isExists == true);

            if (added)
            {
                MintFinancialExport.Core.DataAccess.DeleteItem<Account>(item.ObjectId);
            }
        }

        [Test]
        public void DoesGetNextRunIdGetCorrectly()
        {
            int? nextRunId = 0;
            var latestRun = MintFinancialExport.Core.DataAccess.GetList<AccountHistory>().OrderByDescending(a => a.RunId).FirstOrDefault();
            if (latestRun != null) nextRunId = latestRun.RunId + 1;

            int? nextRunIdCompare = MintFinancialExport.Core.DataAccess.GetNextRunId();

            Assert.That(nextRunId == nextRunIdCompare);
        }

        [Test]
        public void verify_previousrunid_value()
        {
            // Arrange
            int? currentRunId = 0;
            currentRunId = MintFinancialExport.Core.DataAccess.GetCurrentRunId();

            // Act
            int? previousRunId = MintFinancialExport.Core.DataAccess.GetPreviousRunId(currentRunId);

            // Assert
            if (currentRunId == 0)
                Assert.LessOrEqual(previousRunId, currentRunId);
            else
                Assert.Less(previousRunId, currentRunId);
        }
    }
}
