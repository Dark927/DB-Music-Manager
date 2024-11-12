using System.Data;
using System.IO;
using MusicManager.DBManagement.Query;
using MusicManager.Utilities;

namespace MusicManager.DBManagement
{
    internal class MusicDataProvider : DBDataProviderBase<DataListType>
    {
        private string _statePath = Path.Combine(JsonDataManager.DefaultSavePathDict[SaveDataType.Query], "musicDataProvider.json");
        private DBQueryCollection<DataListType> _queryCollection;


        public MusicDataProvider(string connectionString) : base(connectionString)
        {
            _queryCollection = new DBQueryCollection<DataListType>();
            _queryCollection.LoadStateFromJson(_statePath).ToString();
        }

        public override DataTable RequestData(DataListType type, params int[] parameters)
        {
            DataTable requestedData = null;
            
            if (_queryCollection.ContainKey(type))
            {
                _queryCollection[type].Parameters = parameters;
                requestedData = DataBase.SendQuery(_queryCollection[type]);
            }
            return requestedData;
        }

        public override bool IsDataAvailable(DataListType type, params int[] parameters)
        {
            bool isAvailable = false;

            if (_queryCollection.ContainKey(type))
            {
                _queryCollection[type].Parameters = parameters;
                isAvailable = DataBase.SendQuery(_queryCollection[type]).DefaultView.Count != 0;
            }

            return isAvailable;
        }
    }
}
