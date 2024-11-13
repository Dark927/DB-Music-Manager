
namespace MusicManager.DBManagement
{
    internal class DBToolBase
    {
        private DataBase _dataBase;

        protected DataBase DataBase { get => _dataBase; }

        public DBToolBase(DataBase dataBase)
        {
            _dataBase = dataBase;
        }

    }
}
