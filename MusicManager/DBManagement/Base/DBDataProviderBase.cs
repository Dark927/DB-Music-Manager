using System;
using System.Data;

namespace MusicManager.DBManagement
{
    internal abstract class DBDataProviderBase<TData> : DBManagerBase where TData : Enum
    {
        protected DBDataProviderBase(string connectionString) : base(connectionString) { }

        /// <summary>
        /// request the data from DataBase, throws an exception if an error occurs
        /// </summary>
        /// <param name="type">type of the data to get</param>
        /// <param name="parameters">additional parameters for the query, can be empty</param>
        /// <returns>DataTable with the requested data from the DataBase</returns>
        abstract public DataTable RequestData(TData type, params int[] parameters);

        /// <summary>
        /// check if data is available in the DataBase
        /// </summary>
        /// <param name="type">type of the data to check</param>
        /// <param name="parameters">additional parameters for the query, can be empty</param>
        /// <returns>true - if the data is available, else - false</returns>
        abstract public bool IsDataAvailable(TData type, params int[] parameters);
    }
}
