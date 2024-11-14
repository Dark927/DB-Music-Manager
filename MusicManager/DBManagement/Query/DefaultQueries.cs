using MusicManager.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicManager.DBManagement.Query
{
    internal static class DefaultQueries<T> where T : Enum
    {
        private readonly static Dictionary<Type, DBQueryCollection<T>> DefaultQueriesDict;

        static DefaultQueries()
        {
            DefaultQueriesDict = new Dictionary<Type, DBQueryCollection<T>>();
            DefaultQueriesDict[typeof(MusicDataType)] = new();
            DefaultQueriesDict[typeof(AuthorDataType)] = new();
            DefaultQueriesDict[typeof(AuthorMusicDataType)] = new();

            AddDefaultQuery(typeof(MusicDataType), (T)(object)MusicDataType.Default, new DBQuery(@"SELECT * FROM Music;"));
            
            
            AddDefaultQuery(typeof(AuthorDataType), (T)(object)AuthorDataType.Default, new DBQuery(@"SELECT * FROM Author;"));
            
            
            AddDefaultQuery(typeof(AuthorMusicDataType), (T)(object)AuthorMusicDataType.Default, new DBQuery(@"SELECT * FROM Music m inner join AuthorMusic am on m.Id = am.MusicId where am.AuthorId = @AuthorId;"));
        }

        public static DBQueryCollection<T> RequestDefaultQueries()
        {
            DBQueryCollection<T> queriesCollection = new DBQueryCollection<T>();

            if (DefaultQueriesDict.ContainsKey(typeof(T)))
            {
                queriesCollection = DefaultQueriesDict[typeof(T)];
            }

            return queriesCollection;
        }

        private static void AddDefaultQuery(Type dataType, T key, DBQuery query)
        {
            var queryCollection = DefaultQueriesDict[dataType];
            queryCollection.AddQuery(key, query);
        }
    }
}
