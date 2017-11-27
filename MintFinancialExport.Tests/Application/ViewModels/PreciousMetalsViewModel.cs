using System;
using System.Collections.Generic;
using MintFinancialExport.Core;
using MintFinancialExport.Core.Interfaces;
using NUnit.Framework;
using Moq;

namespace MintFinancialExport.Tests.Application.ViewModels
{
    [TestFixture]
    class PreciousMetalsViewModel
    {
        [SetUp]
        public void SetUp()
        {
            var dataAccessMock = new Mock<IDataAccess>();

            dataAccessMock.Setup(x => x.GetNextRunId()).Returns(0);
            dataAccessMock.Setup(x => x.GetList<PreciousMetalsHistory>())
                .Returns(new List<PreciousMetalsHistory>());

            ServiceLocator.AddItem(dataAccessMock.Object);

            // Should I mock this so I don't make a web client call?
            IPreciousMetalsPriceApi preciousMetalsPriceApi = new PreciousMetalsPriceApi();
            ServiceLocator.AddItem(typeof(IPreciousMetalsPriceApi), preciousMetalsPriceApi);
        }

        [Test]
        public void should_gettotals_return_correctly()
        {
            decimal? total;

            // Arrange
            WPF.ViewModels.PreciousMetalsViewModel sut = new WPF.ViewModels.PreciousMetalsViewModel();
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
