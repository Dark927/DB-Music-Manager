using System;


namespace MusicManager.DBManagement.ManagementKits
{
    internal class DBManagementKit<T> where T : Enum
    {
        private DataBase _dataBase;
        private DataProvider<T> _dataProvider;

        public DBManagementKit(DataBase dataBase)
        {
            _dataBase = dataBase;
        }

        public DataProvider<T> RequestDataProvider()
        {
            if (_dataProvider == null)
            {
                _dataProvider = new DataProvider<T>(_dataBase);
            }

            return _dataProvider;
        }
    }
}
