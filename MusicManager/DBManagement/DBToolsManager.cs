using MusicManager.DBManagement.ManagementKits;
using System;
using System.Collections.Generic;
using System.Data;


namespace MusicManager.DBManagement
{
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

            var dataProvider = managementKit.RequestDataProvider();
            requestedData = dataProvider.RequestData(type, parameters);

            return requestedData;
        }


        private DBManagementKit<T> GetManagementKit<T>() where T : Enum
        {
            Type typeKey = typeof(T);
            DBManagementKit<T> requestedKit = null;

            if (_managementKitsMap.ContainsKey(typeKey))
            {
                requestedKit = (DBManagementKit<T>)_managementKitsMap[typeKey];
            }
            else
            {
                requestedKit = new DBManagementKit<T>(_dataBase);
                _managementKitsMap.Add(typeKey, requestedKit);
            }

            return requestedKit;
        }
    }
}
