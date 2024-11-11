
using System.Linq;
using System.Text.Json.Serialization;

namespace MusicManager.DBManagement
{
    internal class DBQuery
    {
        public string Text { get; }
        public int[] Parameters { get; set; }

        public DBQuery(string text, params int[] parameters)
        {
            Text = text;
            Parameters = parameters;
        }
    }
}
