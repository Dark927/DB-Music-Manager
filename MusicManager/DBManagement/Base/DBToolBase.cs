
using System;

namespace MusicManager.DBManagement
{
    internal abstract class DBToolBase<T> where T : Enum
    {
        private DataBase _dataBase;

        protected DataBase DB { get => _dataBase; }

        public DBToolBase(DataBase dataBase)
        {
            _dataBase = dataBase;
        }
    }
}
