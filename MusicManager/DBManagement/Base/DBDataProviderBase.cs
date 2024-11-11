using System;
using System.Data;

namespace MusicManager.DBManagement
{
    internal abstract class DBDataProviderBase<TData> : DBManagerBase where TData : Enum
    {
        protected DBDataProviderBase(string connectionString) : base(connectionString) { }

        abstract public DataTable TryRequestData(TData type, params int[] parameters);
        abstract public bool IsDataAvailable(TData type, params int[] parameters);
    }
}
