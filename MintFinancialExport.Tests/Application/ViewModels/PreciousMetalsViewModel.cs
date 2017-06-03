using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintFinancialExport.Tests.Application.ViewModels
{
    [TestFixture]
    class PreciousMetalsViewModel
    {
        [Test]
        public void should_gettotals_return_correctly()
        {
            decimal? total;

            // Arrange
            MintFinancialExport.ViewModels.PreciousMetalsViewModel sut = new MintFinancialExport.ViewModels.PreciousMetalsViewModel();
            sut.GoldOunces = 1;
            sut.GoldSpotPrice = 100;
            sut.SilverOunces = 1;
            sut.SilverSpotPrice = 100;
            sut.PlatinumOunces = 1;
            sut.PlatinumSpotPrice = 100;
            sut.PalladiumOunces = 1;
            sut.PalladiumSpotPrice = 100;

            // Act
            total = sut.GetTotals();

            // Assert
            Assert.That(total == 400);
            
        }
    }
}
