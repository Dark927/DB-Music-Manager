using MusicManager.DBManagement.Query;
using System;


namespace MusicManager.DBManagement
{
    class DataRemover<T> : DBToolBase<T> where T : Enum
    {

        public DataRemover(DataBase dataBase, DBQueryCollection<T> queriesCollection) : base(dataBase, queriesCollection)
        {

        }

        /// <summary>
        /// delete the data from the DataBase, throws an exception if an error occurs
        /// </summary>
        /// <param name="type">type of the data to delete</param>
        /// <param name="parameters">additional parameters for the query, can be empty</param>
        public void DeleteData(T type, params string[] parameters)
        {
            if (QueriesCollection.ContainKey(type))
            {
                QueriesCollection[type].Parameters = parameters;
                DB.SendQuery(QueriesCollection[type]);
            }

        }
    }
}
