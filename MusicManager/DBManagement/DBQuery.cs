
using System.Linq;

namespace MusicManager.DBManagement
{
    internal class DBQuery
    {
        public string Text { get; }
        public int[] Parameters { get; }

        public DBQuery(string query, params int[] parameters)
        {
            Text = query;
            Parameters = parameters;
        }
    }
}
