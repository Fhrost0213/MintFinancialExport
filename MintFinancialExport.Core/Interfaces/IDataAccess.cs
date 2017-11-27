using System.Collections.Generic;

namespace MintFinancialExport.Core.Interfaces
{
    public interface IDataAccess
    {
        void SaveList<T>(List<T> itemList) where T : class;
        void SaveItem<T>(T item) where T : class;
        List<T> GetList<T>() where T : class;
        T GetItem<T>(int objectId) where T: class, Core.Interfaces.IObjectIdEntity;
        void DeleteItem<T>(int objectId) where T: class, Core.Interfaces.IObjectIdEntity;
        void DeleteItem<T>(T item) where T : class;
        bool DoesItemExist<T>(int objectId) where T : class, Core.Interfaces.IObjectIdEntity;
        bool DoesItemExist<T>(T item) where T : class, Core.Interfaces.IObjectIdEntity;
        int? GetAccountTypeIdFromAccountName(string accountName);
        User GetUserFromUserName(string userName);
        int? GetNextRunId();
        int? GetCurrentRunId();
        int? GetPreviousRunId(int? runId);
        string GetOption(string optionName);
        void SaveOption(string optionKey, string optionValue);
    }
}