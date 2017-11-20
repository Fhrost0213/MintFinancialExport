using MintFinancialExport.Core;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MintFinancialExport.Tests.Core
{
    [TestFixture]
    public class DataAccess
    {
        private IDataAccess _dataAccess;

        [SetUp]
        public void SetUp()
        {
            _dataAccess = ServiceLocator.GetInstance<IDataAccess>();
        }

        [Test]
        public void GetListOfAccount()
        {
            var list = _dataAccess.GetList<Account>();
            Assert.IsInstanceOf<List<Account>>(list);
        }

        [Test]
        public void GetListOfAccountHistory()
        {
            var list = _dataAccess.GetList<AccountHistory>();
            Assert.IsInstanceOf<List<AccountHistory>>(list);
        }

        [Test]
        public void GetListOfAccountMapping()
        {
            var list = _dataAccess.GetList<AccountMapping>();
            Assert.IsInstanceOf<List<AccountMapping>>(list);
        }

        [Test]
        public void GetListOfAccountType()
        {
            var list = _dataAccess.GetList<AccountType>();
            Assert.IsInstanceOf<List<AccountType>>(list);
        }

        [Test]
        public void SaveAndDeleteListOfAccount()
        {
            Account account = new Account();
            string name = "NUnit Tests - SaveListOfAccount";
            account.AccountName = name;

            _dataAccess.SaveItem(account);

            var savedItem = _dataAccess.GetList<Account>().Where(n => n.AccountName == name).First();

            Assert.That(savedItem != null);

            _dataAccess.DeleteItem<Account>(savedItem.ObjectId);

            Assert.That(_dataAccess.DoesItemExist<Account>(savedItem.ObjectId) == false);
        }

        [Test]
        public void DoesAccountExist()
        {
            bool added = false;

            var item = _dataAccess.GetList<Account>().FirstOrDefault();

            if (item == null)
            {
                Account account = new Account();
                account.AccountName = "NUnit Tests - DoesAccountExist";

                _dataAccess.SaveItem(account);
                added = true;
            }

            item = _dataAccess.GetList<Account>().FirstOrDefault();

            bool isExists = _dataAccess.DoesItemExist<Account>(item.ObjectId);
            Assert.That(isExists == true);

            if (added)
            {
                _dataAccess.DeleteItem<Account>(item.ObjectId);
            }
        }

        [Test]
        public void DoesGetNextRunIdGetCorrectly()
        {
            int? nextRunId = 0;
            var latestRun = _dataAccess.GetList<AccountHistory>().OrderByDescending(a => a.RunId).FirstOrDefault();
            if (latestRun != null) nextRunId = latestRun.RunId + 1;

            int? nextRunIdCompare = _dataAccess.GetNextRunId();

            Assert.That(nextRunId == nextRunIdCompare);
        }

        [Test]
        public void verify_previousrunid_value()
        {
            // Arrange
            int? currentRunId = 0;
            currentRunId = _dataAccess.GetCurrentRunId();

            // Act
            int? previousRunId = _dataAccess.GetPreviousRunId(currentRunId);

            // Assert
            if (currentRunId == 0)
                Assert.LessOrEqual(previousRunId, currentRunId);
            else
                Assert.Less(previousRunId, currentRunId);
        }
    }
}
