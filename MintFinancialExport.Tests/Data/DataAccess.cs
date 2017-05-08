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

            MintFinancialExport.Data.DataAccess.DeleteItemByObjectId<Account>(savedItem.ObjectId);

            Assert.That(MintFinancialExport.Data.DataAccess.DoesItemExistByObjectId<Account>(savedItem.ObjectId) == false);
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

            bool isExists = MintFinancialExport.Data.DataAccess.DoesItemExistByObjectId<Account>(item.ObjectId);
            Assert.That(isExists == true);

            if (added)
            {
                MintFinancialExport.Data.DataAccess.DeleteItemByObjectId<Account>(item.ObjectId);
            }
        }
    }
}
