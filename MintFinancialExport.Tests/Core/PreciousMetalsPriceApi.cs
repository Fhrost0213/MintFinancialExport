using MintFinancialExport.Core;
using MintFinancialExport.Core.Interfaces;
using NUnit.Framework;

namespace MintFinancialExport.Tests.Core
{
    [TestFixture]
    class PreciousMetals_api
    {
        private IPreciousMetalsPriceApi _api;

        [SetUp]
        public void SetUp()
        {
            // Do I want to mock this to not make a web client call?
            IPreciousMetalsPriceApi api = new PreciousMetalsPriceApi();
            ServiceLocator.AddItem(typeof(IPreciousMetalsPriceApi), api);

            _api = ServiceLocator.GetInstance<IPreciousMetalsPriceApi>();
        }

        [Test]
        public void Does_GetPreciousMetalsPrice_Gold_Return_Value()
        {
            var value = _api.GetPreciousMetalsPrice(MintFinancialExport.Core.Entities.Enums.PreciousMetalsTypes.Gold);
            Assert.That(value > 0);
        }

        [Test]
        public void Does_GetPreciousMetalsPrice_Silver_Return_Value()
        {
            var value = _api.GetPreciousMetalsPrice(MintFinancialExport.Core.Entities.Enums.PreciousMetalsTypes.Silver);
            Assert.That(value > 0);
        }

        [Test]
        public void Does_GetPreciousMetalsPrice_Platinum_Return_Value()
        {
            var value = _api.GetPreciousMetalsPrice(MintFinancialExport.Core.Entities.Enums.PreciousMetalsTypes.Platinum);
            Assert.That(value > 0);
        }

        [Test]
        public void Does_GetPreciousMetalsPrice_Palladium_Return_Value()
        {
            var value = _api.GetPreciousMetalsPrice(MintFinancialExport.Core.Entities.Enums.PreciousMetalsTypes.Palladium);
            Assert.That(value > 0);
        }
    }
}
