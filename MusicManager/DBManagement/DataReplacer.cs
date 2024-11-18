using MusicManager.DBManagement.Query;
using System;
using System.Data;

namespace MusicManager.DBManagement
{
    class DataReplacer<T> : DBToolBase<T> where T : Enum
    {
        public DataReplacer(DataBase dataBase, DBQueryCollection<T> queriesCollection) : base(dataBase, queriesCollection)
        {

        }

        /// <summary>
        /// replace the data in the DataBase, throws an exception if an error occurs
        /// </summary>
        /// <param name="type">type of the data to replace</param>
        /// <param name="parameters">parameters for the query</param>
        public void ReplaceData(T type, params string[] parameters)
        {
            if (QueriesCollection.ContainKey(type))
            {
                QueriesCollection[type].Parameters = parameters;
                DB.SendQuery(QueriesCollection[type]);
            }
        }
    }
}
