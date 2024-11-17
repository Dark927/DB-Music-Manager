using System;


namespace MusicManager.DBManagement.Query
{
    internal class DBQueriesContainer<T> where T : Enum
    {
        public DBQueryCollection<T> RequestDataQueries { get; set; }
        public DBQueryCollection<T> AddDataQueries { get; set; }
        public DBQueryCollection<T> RemoveDataQueries { get; set; }
        public DBQueryCollection<T> UpdateDataQueries { get; set; }

        public DBQueriesContainer()
        {

        }

        public void Init()
        {
            RequestDataQueries = new DBQueryCollection<T>();
            AddDataQueries = new DBQueryCollection<T>();
            RemoveDataQueries = new DBQueryCollection<T>();
            UpdateDataQueries = new DBQueryCollection<T>();
        }
    }
}
