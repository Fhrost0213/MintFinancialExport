using MintFinancialExport.Core;
using StructureMap;

namespace MintFinancialExport.WPF
{
    public static class Bootstrapper
    {
        public static void ConfigureStructureMap()
        {
            IDataAccess dataAccess = new DataAccess();
            ServiceLocator.AddItem(typeof(IDataAccess), dataAccess);
        }
    }
}
