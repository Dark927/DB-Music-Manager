using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace MusicManager.DBManagement.Query
{
    internal sealed class DBQueryCollection<TData> where TData : Enum
    {
        [JsonInclude]
        private Dictionary<TData, DBQuery> _queriesDict;

        public DBQueryCollection()
        {
            CreateEmpty();
        }

        public DBQueryCollection(Dictionary<TData, DBQuery> queriesDict)
        {
            _queriesDict = queriesDict;
        }

        public void CreateEmpty()
        {
            _queriesDict = new Dictionary<TData, DBQuery>();
        }

        public DBQuery this[TData key]
        {
            get => _queriesDict.ContainsKey(key) ? _queriesDict[key] : null;
        }

        public void RemoveQuery(TData key)
        {
            _queriesDict.Remove(key);
        }

        public void AddQuery(TData key, DBQuery value)
        {
            _queriesDict.TryAdd(key, value);
        }

        public bool ContainKey(TData key)
        {
            return _queriesDict.ContainsKey(key);
        }
    }
}

