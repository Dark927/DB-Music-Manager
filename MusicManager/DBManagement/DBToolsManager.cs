using MusicManager.DBManagement.ManagementKits;
using MusicManager.DBManagement.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Windows;


namespace MusicManager.DBManagement
{
    public enum MusicDataType
    {
        Default,
        Id,
        Title,
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

        private delegate void ToolOperationDelegate<TDataType>(DBToolBase<TDataType> tool, TDataType dataType, params string[] parameters) where TDataType : Enum;

        public DBToolsManager(string connectionString)
        {
            _dataBase = new DataBase(connectionString);

            if (_dataBase == null)
            {
                throw new ArgumentException("Connection string is invalid, DataBase == null");
            }

            _managementKitsMap = new();
        }

        public DataTable RequestData<TDataType>(TDataType type, params string[] parameters) where TDataType : Enum
        {
            DataTable requestedData;

            DBManagementKit<TDataType> managementKit = GetManagementKit<TDataType>();

            var dataProvider = (DataProvider<TDataType>)managementKit.RequestTool(ToolType.DataProvider);
            requestedData = dataProvider.RequestData(type, parameters);

            return requestedData;
        }

        public void UpdateData<TDataType>(ToolType toolType, TDataType dataType, params string[] parameters) where TDataType : Enum
        {
            try
            {
                DBManagementKit<TDataType> managementKit = GetManagementKit<TDataType>();
                var tool = managementKit.RequestTool(toolType);
                var operation = GetToolOperation<TDataType>(toolType);

                operation.Invoke(tool, dataType, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void RegisterProviderQuery<T>(T type, DBQuery query) where T : Enum
        {
            string kitDefaultPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{typeof(T)}-queriesContainer.json");

            Type typeKey = typeof(T);

            DBManagementKit<T> managementKit = GetManagementKit<T>();

            managementKit.AddDataProviderQuery(type, query);
            managementKit.SaveStateInJson(kitDefaultPath);
        }

        private ToolOperationDelegate<TDataType> GetToolOperation<TDataType>(ToolType toolType) where TDataType : Enum
        {
            return toolType switch
            {
                ToolType.DataRemover => DeleteData,
                ToolType.DataAdder => AddData,
                ToolType.DataReplacer => ReplaceData,

                _ => throw new NotImplementedException()
            };
        }

        private void DeleteData<TDataType>(DBToolBase<TDataType> tool, TDataType dataType, params string[] parameters) where TDataType : Enum
        {
            DataRemover<TDataType> dataRemover = (DataRemover<TDataType>)tool;
            dataRemover.DeleteData(dataType, parameters);
        }

        private void AddData<TDataType>(DBToolBase<TDataType> tool, TDataType dataType, params string[] parameters) where TDataType : Enum
        {
            DataAdder<TDataType> dataAdder = (DataAdder<TDataType>)tool;
            dataAdder.AddData(dataType, parameters);
        }

        private void ReplaceData<TDataType>(DBToolBase<TDataType> tool, TDataType dataType, params string[] parameters) where TDataType : Enum
        {
            DataReplacer<TDataType> dataReplacer = (DataReplacer<TDataType>)tool;
            dataReplacer.ReplaceData(dataType, parameters);
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

