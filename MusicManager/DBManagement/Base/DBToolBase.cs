
using MusicManager.DBManagement.Query;
using System;

namespace MusicManager.DBManagement
{
    internal abstract class DBToolBase<T> where T : Enum
    {
        private DataBase _dataBase;
        private DBQueryCollection<T> _queryCollection;

        protected DataBase DB { get => _dataBase; }

        public DBQueryCollection<T> QueriesCollection { get => _queryCollection; set => _queryCollection = value; }


        public DBToolBase(DataBase dataBase, DBQueryCollection<T> queriesCollection)
        {
            _dataBase = dataBase;
            QueriesCollection = queriesCollection;
        }
    }
}
