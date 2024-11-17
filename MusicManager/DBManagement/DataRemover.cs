using MusicManager.DBManagement.Query;
using System;


namespace MusicManager.DBManagement
{
    class DataRemover<T> : DBToolBase<T> where T : Enum
    {
        private DBQueryCollection<T> _queryCollection;

        public DataRemover(DataBase dataBase, DBQueryCollection<T> queriesCollection) : base(dataBase)
        {
            SetQueriesCollection(queriesCollection);
        }

        public void SetQueriesCollection(DBQueryCollection<T> state)
        {
            _queryCollection = state;
        }


        /// <summary>
        /// delete the data from the DataBase, throws an exception if an error occurs
        /// </summary>
        /// <param name="type">type of the data to delete</param>
        /// <param name="parameters">additional parameters for the query, can be empty</param>
        public void DeleteData(T type, params int[] parameters)
        {
            if (_queryCollection.ContainKey(type))
            {
                _queryCollection[type].Parameters = parameters;
                DB.SendQuery(_queryCollection[type]);
            }

        }
    }
}
