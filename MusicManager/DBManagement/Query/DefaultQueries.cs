using MusicManager.DBManagement.ManagementKits;
using System;
using System.Collections.Generic;

namespace MusicManager.DBManagement.Query
{
    internal static class DefaultQueries<T> where T : Enum
    {
        private readonly static Dictionary<Type, DBQueriesContainer<T>> DefaultQueriesDict;
        private static Type MusicType = typeof(MusicDataType);
        private static Type AuthorType = typeof(AuthorDataType);
        private static Type AuthorMusicType = typeof(AuthorMusicDataType);

        static DefaultQueries()
        {
            DefaultQueriesDict = new Dictionary<Type, DBQueriesContainer<T>>();
            DefaultQueriesDict[typeof(MusicDataType)] = new();
            DefaultQueriesDict[typeof(AuthorDataType)] = new();
            DefaultQueriesDict[typeof(AuthorMusicDataType)] = new();

            Init();


            // ------------------------------
            // Data Provider
            // ------------------------------

            AddDefaultQuery(ToolType.DataProvider, MusicType, (T)(object)MusicDataType.Default, new DBQuery(@"SELECT * FROM Music;"));
            AddDefaultQuery(ToolType.DataProvider, AuthorType, (T)(object)AuthorDataType.Default, new DBQuery(@"SELECT * FROM Author;"));
            AddDefaultQuery(ToolType.DataProvider, AuthorMusicType, (T)(object)AuthorMusicDataType.Default, new DBQuery(@"SELECT * FROM Music m inner join AuthorMusic am on m.Id = am.MusicId where am.AuthorId = @AuthorId;"));


            // ------------------------------
            // Data Remover
            // ------------------------------


            AddDefaultQuery(ToolType.DataRemover, MusicType, (T)(object)MusicDataType.Default, new DBQuery(@"DELETE FROM Music WHERE Music.Id = @param;"));
            AddDefaultQuery(ToolType.DataRemover, AuthorType, (T)(object)AuthorDataType.Default, new DBQuery(@"DELETE FROM Author WHERE Author.Id = @param;"));
            AddDefaultQuery(ToolType.DataRemover, AuthorMusicType, (T)(object)AuthorMusicDataType.Default, new DBQuery(@"DELETE FROM AuthorMusic WHERE AuthorMusic.MusicId = @param;"));


            // ------------------------------
            // Data Adder
            // ------------------------------

            AddDefaultQuery(ToolType.DataAdder, MusicType, (T)(object)MusicDataType.Default, new DBQuery(@"INSERT INTO Music VALUES (@title, @duration, @style);"));
            AddDefaultQuery(ToolType.DataAdder, AuthorType, (T)(object)AuthorDataType.Default, new DBQuery(@"INSERT INTO Author VALUES (@name);"));
            AddDefaultQuery(ToolType.DataAdder, AuthorMusicType, (T)(object)AuthorDataType.Default, new DBQuery(@"INSERT INTO AuthorMusic VALUES (@musicId, @authorId);"));


            // ------------------------------
            // Data Replacer
            // ------------------------------

            AddDefaultQuery(ToolType.DataReplacer, MusicType, (T)(object)MusicDataType.Default, new DBQuery(@"UPDATE Music SET Title = @title WHERE Id = @id;"));
            AddDefaultQuery(ToolType.DataReplacer, AuthorType, (T)(object)AuthorDataType.Default, new DBQuery(@"UPDATE Author SET Name = @name WHERE Id = @id;"));

        }

        public static DBQueryCollection<T> RequestQueriesCollection(ToolType toolType, Type dataType)
        {
            DBQueryCollection<T> queriesCollection = GetQueriesCollection(toolType, dataType);
            return queriesCollection;
        }

        public static DBQueriesContainer<T> RequestQueriesContainer()
        {
            return DefaultQueriesDict[typeof(T)];
        }

        private static void Init()
        {
            DefaultQueriesDict[typeof(MusicDataType)].Init();
            DefaultQueriesDict[typeof(AuthorDataType)].Init();
            DefaultQueriesDict[typeof(AuthorMusicDataType)].Init();
        }

        private static void AddDefaultQuery(ToolType toolType, Type dataType, T key, DBQuery query)
        {
            DBQueryCollection<T> requestedQueries = GetQueriesCollection(toolType, dataType);
            requestedQueries.AddQuery(key, query);
        }

        private static DBQueryCollection<T> GetQueriesCollection(ToolType toolType, Type dataType)
        {
            var queriesCollection = DefaultQueriesDict[dataType];

            return toolType switch
            {
                ToolType.DataProvider => queriesCollection.RequestDataQueries,
                ToolType.DataRemover => queriesCollection.RemoveDataQueries,
                ToolType.DataAdder => queriesCollection.AddDataQueries,
                ToolType.DataReplacer => queriesCollection.ReplaceDataQueries,

                _ => throw new NotImplementedException()
            };
        }
    }
}

