using System;
using System.Data.Linq;
using System.Linq;

namespace MusicViewer.Scripts
{
    public class DBDataContext
    {
        private string _connectionString;
        private static Lazy<DBDataContext> _instance = new Lazy<DBDataContext>(() => new DBDataContext());

        private DBDataContext() { }

        public static DBDataContext Instance => _instance.Value;

        public MusicDataClassesDataContext GetContext()
        {
            return new MusicDataClassesDataContext(_connectionString);
        }

        public static bool HasContextChanges(DataContext context)
        {
            var contextChangeSet = context.GetChangeSet();
            return contextChangeSet.Inserts.Any() || contextChangeSet.Deletes.Any() || contextChangeSet.Updates.Any();
        }

        public void InitDataContext(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
