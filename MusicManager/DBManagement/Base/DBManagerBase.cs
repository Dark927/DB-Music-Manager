
namespace MusicManager.DBManagement
{
    internal abstract class DBManagerBase
    {
        private DataBase _dataBase;

        protected DataBase DataBase { get => _dataBase; }

        public DBManagerBase(string connectionString)
        {
            _dataBase = new DataBase(connectionString);
        }
    }
}
