﻿using MintFinancialExport.Core;
using MintFinancialExport.Core.Interfaces;
using MintFinancialExport.WPF.Interfaces;
using StructureMap;

namespace MintFinancialExport.WPF
{
    public static class Bootstrapper
    {
        public static void ConfigureStructureMap()
        {
            IDataAccess dataAccess = new DataAccess();
            ServiceLocator.AddItem(typeof(IDataAccess), dataAccess);

            IEntitySync entitySync = new EntitySync();
            ServiceLocator.AddItem(typeof(IEntitySync), entitySync);

            IPreciousMetalsPriceApi preciousMetalsPriceApi = new PreciousMetalsPriceApi();
            ServiceLocator.AddItem(typeof(IPreciousMetalsPriceApi), preciousMetalsPriceApi);

            IAccountInfoHandler accountInfoHandler = new AccountInfoHandler();
            ServiceLocator.AddItem(typeof(IAccountInfoHandler), accountInfoHandler);

            IMintApi mintApi = new MintApi();
            ServiceLocator.AddItem(typeof(IMintApi), mintApi);
        }
    }
}
