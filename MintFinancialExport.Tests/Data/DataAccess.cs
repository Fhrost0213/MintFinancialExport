using MintFinancialExport.Data;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintFinancialExport.Tests.Data
{
    [TestFixture]
    public class DataAccess
    {
        [Test]
        public void GetListOfAccount()
        {
            var list = MintFinancialExport.Data.DataAccess.GetList<Account>();
            Assert.IsInstanceOf<List<Account>>(list);
        }

        [Test]
        public void GetListOfAccountHistory()
        {
            var list = MintFinancialExport.Data.DataAccess.GetList<AccountHistory>();
            Assert.IsInstanceOf<List<AccountHistory>>(list);
        }

        [Test]
        public void GetListOfAccountMapping()
        {
            var list = MintFinancialExport.Data.DataAccess.GetList<AccountMapping>();
            Assert.IsInstanceOf<List<AccountMapping>>(list);
        }

        [Test]
        public void GetListOfAccountType()
        {
            var list = MintFinancialExport.Data.DataAccess.GetList<AccountType>();
            Assert.IsInstanceOf<List<AccountType>>(list);
        }

        [Test]
        public void SaveAndDeleteListOfAccount()
        {
            Account account = new Account();
            string name = "NUnit Tests - SaveListOfAccount";
            account.AccountName = name;

            MintFinancialExport.Data.DataAccess.SaveItem(account);

            var savedItem = MintFinancialExport.Data.DataAccess.GetList<Account>().Where(n => n.AccountName == name).First();

            Assert.That(savedItem != null);

            MintFinancialExport.Data.DataAccess.DeleteItem<Account>(savedItem.ObjectId);

            Assert.That(MintFinancialExport.Data.DataAccess.DoesItemExist<Account>(savedItem.ObjectId) == false);
        }

        [Test]
        public void DoesAccountExist()
        {
            bool added = false;

            var item = MintFinancialExport.Data.DataAccess.GetList<Account>().FirstOrDefault();

            if (item == null)
            {
                Account account = new Account();
                account.AccountName = "NUnit Tests - DoesAccountExist";

                MintFinancialExport.Data.DataAccess.SaveItem(account);
                added = true;
            }

            item = MintFinancialExport.Data.DataAccess.GetList<Account>().FirstOrDefault();

            bool isExists = MintFinancialExport.Data.DataAccess.DoesItemExist<Account>(item.ObjectId);
            Assert.That(isExists == true);

            if (added)
            {
                MintFinancialExport.Data.DataAccess.DeleteItem<Account>(item.ObjectId);
            }
        }

        [Test]
        public void DoesGetNextRunIdGetCorrectly()
        {
            int? nextRunId = 0;
            var latestRun = MintFinancialExport.Data.DataAccess.GetList<AccountHistory>().OrderByDescending(a => a.RunId).FirstOrDefault();
            if (latestRun != null) nextRunId = latestRun.RunId + 1;

            int? nextRunIdCompare = MintFinancialExport.Data.DataAccess.GetNextRunId();

            Assert.That(nextRunId == nextRunIdCompare);
        }

        [Test]
        public void verify_previousrunid_value()
        {
            // Arrange
            int? currentRunId = 0;
            currentRunId = MintFinancialExport.Data.DataAccess.GetCurrentRunId();

            // Act
            int? previousRunId = MintFinancialExport.Data.DataAccess.GetPreviousRunId(currentRunId);

            // Assert
            if (currentRunId == 0)
                Assert.LessOrEqual(previousRunId, currentRunId);
            else
                Assert.Less(previousRunId, currentRunId);
        }
    }
}
