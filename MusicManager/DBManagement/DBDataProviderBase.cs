using System;
using System.Data;

namespace MusicManager.DBManagement
{
    internal abstract class DBDataProviderBase<TData> where TData : Enum
    {
        private DataBaseManager _dataBaseManager;

        protected DataBaseManager DataBaseManager { get => _dataBaseManager; }

        abstract public DataTable RequestData(TData type, params int[] parameters);

        public DBDataProviderBase(string connectionString)
        {
            _dataBaseManager = new DataBaseManager(connectionString);
        }
    }
}
