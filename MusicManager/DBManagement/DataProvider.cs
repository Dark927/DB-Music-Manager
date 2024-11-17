using System;
using System.Data;
using System.IO;
using MusicManager.DBManagement.Query;
using MusicManager.Utilities;

namespace MusicManager.DBManagement
{
    internal class DataProvider<T> : DBToolBase<T> where T : Enum
    {
        public DataProvider(DataBase dataBase, DBQueryCollection<T> queriesCollection) : base(dataBase, queriesCollection)
        {

        }


        /// <summary>
        /// request the data from DataBase, throws an exception if an error occurs
        /// </summary>
        /// <param name="type">type of the data to get</param>
        /// <param name="parameters">additional parameters for the query, can be empty</param>
        /// <returns>DataTable with the requested data from the DataBase</returns>
        public DataTable RequestData(T type, params string[] parameters)
        {
            DataTable requestedData = null;

            if (QueriesCollection.ContainKey(type))
            {
                QueriesCollection[type].Parameters = parameters;
                requestedData = DB.SendQuery(QueriesCollection[type]);
            }
            return requestedData;
        }


        /// <summary>
        /// check if data is available in the DataBase
        /// </summary>
        /// <param name="type">type of the data to check</param>
        /// <param name="parameters">additional parameters for the query, can be empty</param>
        /// <returns>true - if the data is available, else - false</returns>
        public bool IsDataAvailable(T type, params string[] parameters)
        {
            bool isAvailable = false;

            if (QueriesCollection.ContainKey(type))
            {
                QueriesCollection[type].Parameters = parameters;
                isAvailable = DB.SendQuery(QueriesCollection[type]).DefaultView.Count != 0;
            }

            return isAvailable;
        }
    }
}
