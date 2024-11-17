using MusicManager.DBManagement.ManagementKits;
using MusicManager.DBManagement.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;


namespace MusicManager.DBManagement
{
    public enum MusicDataType
    {
        Default,
        Id,
        Name,
        Duration,
        Style,
    }

    public enum AuthorDataType
    {
        Default,
        Id,
        Name,
    }

    public enum AuthorMusicDataType
    {
        Default,
        Duration,
        Style,
    }

    internal class DBToolsManager
    {
        private readonly DataBase _dataBase;
        private Dictionary<Type, object> _managementKitsMap;

        public DBToolsManager(string connectionString)
        {
            _dataBase = new DataBase(connectionString);

            if (_dataBase == null)
            {
                throw new ArgumentException("Connection string is invalid, DataBase == null");
            }

            _managementKitsMap = new();
        }

        public DataTable RequestData<TDataType>(TDataType type, params int[] parameters) where TDataType : Enum
        {
            DataTable requestedData;

            DBManagementKit<TDataType> managementKit = GetManagementKit<TDataType>();

            var dataProvider = (DataProvider<TDataType>)managementKit.RequestTool(ToolType.DataProvider);
            requestedData = dataProvider.RequestData(type, parameters);

            return requestedData;
        }

        public void DeleteData<TDataType>(TDataType type, params int[] parameters) where TDataType: Enum
        {
            DBManagementKit<TDataType> managementKit = GetManagementKit<TDataType>();

            var dataRemover = (DataRemover<TDataType>)managementKit.RequestTool(ToolType.DataRemover);
            dataRemover.DeleteData(type, parameters);
        }

        public void RegisterProviderQuery<T>(T type, DBQuery query) where T : Enum
        {
            string kitDefaultPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{typeof(T)}-queriesContainer.json");

            Type typeKey = typeof(T);

            DBManagementKit<T> managementKit = GetManagementKit<T>();

            managementKit.AddDataProviderQuery(type, query);
            managementKit.SaveStateInJson(kitDefaultPath);
        }


        private DBManagementKit<T> GetManagementKit<T>() where T : Enum
        {
            string kitDefaultPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{typeof(T)}-queriesContainer.json");

            Type typeKey = typeof(T);
            DBManagementKit<T> requestedKit = null;

            if (_managementKitsMap.ContainsKey(typeKey))
            {
                requestedKit = (DBManagementKit<T>)_managementKitsMap[typeKey];
            }
            else
            {
                requestedKit = new DBManagementKit<T>(_dataBase);

                if (!requestedKit.LoadStateFromJson(kitDefaultPath))
                {
                    requestedKit.SetDefaultQueries();
                }
                _managementKitsMap.Add(typeKey, requestedKit);
            }

            return requestedKit;
        }

    }
}

