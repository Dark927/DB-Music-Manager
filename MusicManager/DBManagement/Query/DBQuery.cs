
using System.Linq;
using System.Text.Json.Serialization;

namespace MusicManager.DBManagement.Query
{
    internal class DBQuery
    {
        public string Text { get; }
        public string[] Parameters { get; set; }


        public DBQuery(string text, params string[] parameters)
        {
            Text = text;
            Parameters = parameters;
        }
    }
}
