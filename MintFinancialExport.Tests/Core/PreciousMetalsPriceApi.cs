using NUnit.Framework;

namespace MintFinancialExport.Tests.Core
{
    [TestFixture]
    class PreciousMetalsPriceApi
    {
        [Test]
        public void Does_GetPreciousMetalsPrice_Gold_Return_Value()
        {
            MintFinancialExport.Core.PreciousMetalsPriceApi priceApi = new MintFinancialExport.Core.PreciousMetalsPriceApi();

            var value = priceApi.GetPreciousMetalsPrice(MintFinancialExport.Core.Entities.Enums.PreciousMetalsTypes.Gold);
            Assert.That(value > 0);
        }

        [Test]
        public void Does_GetPreciousMetalsPrice_Silver_Return_Value()
        {
            MintFinancialExport.Core.PreciousMetalsPriceApi priceApi = new MintFinancialExport.Core.PreciousMetalsPriceApi();

            var value = priceApi.GetPreciousMetalsPrice(MintFinancialExport.Core.Entities.Enums.PreciousMetalsTypes.Silver);
            Assert.That(value > 0);
        }

        [Test]
        public void Does_GetPreciousMetalsPrice_Platinum_Return_Value()
        {
            MintFinancialExport.Core.PreciousMetalsPriceApi priceApi = new MintFinancialExport.Core.PreciousMetalsPriceApi();

            var value = priceApi.GetPreciousMetalsPrice(MintFinancialExport.Core.Entities.Enums.PreciousMetalsTypes.Platinum);
            Assert.That(value > 0);
        }

        [Test]
        public void Does_GetPreciousMetalsPrice_Palladium_Return_Value()
        {
            MintFinancialExport.Core.PreciousMetalsPriceApi priceApi = new MintFinancialExport.Core.PreciousMetalsPriceApi();

            var value = priceApi.GetPreciousMetalsPrice(MintFinancialExport.Core.Entities.Enums.PreciousMetalsTypes.Palladium);
            Assert.That(value > 0);
        }
    }
}
