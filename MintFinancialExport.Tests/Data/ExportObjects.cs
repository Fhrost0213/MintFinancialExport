using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintFinancialExport.Tests.Data
{
    [TestFixture]
    class ExportObjects
    {
        [Test]
        public void GetListOfExportAccount()
        {
            Data.ExportObjects exportObjects = new ExportObjects();
            exportObjects.GetListOfExportAccount();

            Assert.IsInstanceOf<List<Core.Entities.ExportAccount>>(exportObjects);
            Assert.That(exportObjects != null);        
        }
    }
}
