using MusicManager.DBManagement.ManagementKits;
using System;
using System.Data;


namespace MusicManager.DBManagement
{
    internal class DBToolsManager
    {
        private readonly DataBase _dataBase;

        private DBManagementKit<MusicDataType> _musicManagementKit;
        private DBManagementKit<AuthorDataType> _authorManagementKit;
        private DBManagementKit<AuthorDataType> _authorMusicManagementKit;

        public DBToolsManager(string connectionString)
        {
            _dataBase = new DataBase(connectionString);

            if (_dataBase == null)
            {
                throw new ArgumentException("Connection string is invalid, DataBase == null");
            }

            InitAllManagementKits();
        }

        public DataTable RequestData<TDataType>(TDataType type) where TDataType : Enum
        {
            DataTable requestedData;

            DBManagementKit<TDataType> managementKit = GetManagementKit<TDataType>();

            var dataProvider = managementKit.RequestDataProvider();
            requestedData = dataProvider.RequestData(type);

            return requestedData;
        }


        private void InitAllManagementKits()
        {
            InitManagementKit(ref _musicManagementKit);
            InitManagementKit(ref _authorManagementKit);
            InitManagementKit(ref _authorMusicManagementKit);
        }

        private void InitManagementKit<T>(ref DBManagementKit<T> managementKit) where T : Enum
        {
            if(managementKit == null)
            {
                managementKit = new DBManagementKit<T>(_dataBase);
            }
        }

        private DBManagementKit<TDataType> GetManagementKit<TDataType>() where TDataType : Enum
        {
            if (typeof(TDataType) == typeof(MusicDataType))
            {
                return _musicManagementKit as DBManagementKit<TDataType>;
            }
            else if (typeof(TDataType) == typeof(AuthorDataType))
            {
                return _authorManagementKit as DBManagementKit<TDataType>;
            }

            throw new InvalidOperationException($"ManagementKit does not exist for type {typeof(TDataType).Name}");
        }
    }
}
