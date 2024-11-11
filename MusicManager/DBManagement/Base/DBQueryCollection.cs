using System;
using System.Collections.Generic;


namespace MusicManager.DBManagement.Base
{
    internal sealed class DBQueryCollection<TData> where TData : Enum
    {
        private Dictionary<TData, DBQuery> _queriesDict;

        public DBQueryCollection()
        {
            CreateEmpty();
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


        public bool LoadStateFromJson(string path)
        {
            _queriesDict = JsonDataManager.LoadObjectFromJson<Dictionary<TData, DBQuery>>(path);
            bool isLoaded = _queriesDict != null;

            if (!isLoaded)
            {
                _queriesDict = new Dictionary<TData, DBQuery>();
            }
            return _queriesDict != null;
        }

        public void SaveStateInJson(string path)
        {
            JsonDataManager.SaveDataToJson(_queriesDict, path);
        }
    }
}

