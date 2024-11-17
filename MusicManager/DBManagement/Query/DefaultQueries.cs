﻿using MusicManager.DBManagement.ManagementKits;
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


            // Data Provider

            AddDefaultQuery(ToolType.DataProvider, MusicType, (T)(object)MusicDataType.Default, new DBQuery(@"SELECT * FROM Music;"));
            AddDefaultQuery(ToolType.DataProvider, AuthorType, (T)(object)AuthorDataType.Default, new DBQuery(@"SELECT * FROM Author;"));
            AddDefaultQuery(ToolType.DataProvider, AuthorMusicType, (T)(object)AuthorMusicDataType.Default, new DBQuery(@"SELECT * FROM Music m inner join AuthorMusic am on m.Id = am.MusicId where am.AuthorId = @AuthorId;"));

            // ------------------------------

            // Data Remover

            AddDefaultQuery(ToolType.DataRemover, MusicType, (T)(object)MusicDataType.Default, new DBQuery(@"DELETE FROM Music WHERE Music.Id = @param;"));

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
                ToolType.DataUpdater => queriesCollection.UpdateDataQueries,

                _ => throw new NotImplementedException()
            };
        }
    }
}

