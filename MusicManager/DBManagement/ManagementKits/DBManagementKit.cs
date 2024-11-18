using MusicManager.DBManagement.Query;
using MusicManager.Utilities;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace MusicManager.DBManagement.ManagementKits
{
    public enum ToolType
    {
        DataProvider,
        DataRemover,
        DataAdder,
        DataReplacer,
    }

    internal class DBManagementKit<T> where T : Enum
    {
        private DataBase _dataBase;

        [JsonInclude]
        private DBQueriesContainer<T> _queriesContainer;
        private Dictionary<ToolType, DBToolBase<T>> _toolsDict;

        public DBManagementKit(DataBase dataBase, DBQueriesContainer<T> queriesContainer = null)
        {
            _dataBase = dataBase;
            _queriesContainer = queriesContainer;
            _toolsDict = new Dictionary<ToolType, DBToolBase<T>>();
        }

        public void SetDefaultQueries()
        {
            _queriesContainer = DefaultQueries<T>.RequestQueriesContainer();
        }

        public bool LoadStateFromJson(string path)
        {
            _queriesContainer = JsonDataManager.LoadObjectFromJson<DBQueriesContainer<T>>(path);
            bool isLoaded = _queriesContainer != null;

            return isLoaded;
        }

        public void SaveStateInJson(string path)
        {
            JsonDataManager.SaveDataToJson(_queriesContainer, path);
        }

        public DBToolBase<T> RequestTool(ToolType type)
        {
            DBToolBase<T> requestedTool = null;

            if (_toolsDict.ContainsKey(type))
            {
                requestedTool = _toolsDict[type];
            }
            else
            {
                requestedTool = CreateTool(type);
                _toolsDict.Add(type, requestedTool);
            }

            return requestedTool;
        }

        private DBToolBase<T> CreateTool(ToolType type)
        {
            return type switch
            {
                ToolType.DataProvider => new DataProvider<T>(_dataBase, _queriesContainer.RequestDataQueries),
                ToolType.DataRemover => new DataRemover<T>(_dataBase, _queriesContainer.RemoveDataQueries),
                ToolType.DataAdder => new DataAdder<T>(_dataBase, _queriesContainer.AddDataQueries),
                ToolType.DataReplacer => new DataReplacer<T>(_dataBase, _queriesContainer.ReplaceDataQueries),

                _ => throw new NotSupportedException()
            };
        }


        public void AddDataProviderQuery(T type, DBQuery query)
        {
            if (_queriesContainer == null)
            {
                _queriesContainer = new DBQueriesContainer<T>();
            }

            if (_queriesContainer.RequestDataQueries.ContainKey(type))
            {
                _queriesContainer.RequestDataQueries.RemoveQuery(type);
            }
            _queriesContainer.RequestDataQueries.AddQuery(type, query);
        }
    }
}
