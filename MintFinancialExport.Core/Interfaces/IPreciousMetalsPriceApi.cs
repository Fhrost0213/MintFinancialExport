using MintFinancialExport.Core.Entities;

namespace MintFinancialExport.Core.Interfaces
{
    public interface IPreciousMetalsPriceApi
    {
        decimal? GetPreciousMetalsPrice(Enums.PreciousMetalsTypes metalsType);
    }
}