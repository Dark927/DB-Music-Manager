using MusicManager.DBManagement.Query;
using MusicManager.Utilities;
using System;
using System.IO;

namespace MusicManager.DBManagement
{
    internal class MusicDataEditor : DBDataEditorBase<DataListType>
    {
        private string _statePath = Path.Combine(JsonDataManager.DefaultSavePathDict[SaveDataType.Query], "musicDataEditor.json");
        private DBQueryCollection<DataListType> _queryCollection;

        public MusicDataEditor(string connectionString) : base(connectionString)
        {
            _queryCollection = new DBQueryCollection<DataListType>();
            _queryCollection.LoadStateFromJson(_statePath);
        }

        public override void AddData(DataListType type, params int[] parameters)
        {
            _queryCollection[type].Parameters = parameters;
            DataBase.SendQuery(_queryCollection[type]);
        }

        public override bool TryAddData(DataListType type, params int[] parameters)
        {
            bool isAdded = false;

            if (_queryCollection.ContainKey(type))
            {
                _queryCollection[type].Parameters = parameters;
                isAdded = DataBase.SendQuery(_queryCollection[type]).DefaultView.Count != 0;
            }

            return isAdded;
        }

        public override void DeleteData(DataListType type, params int[] parameters)
        {
            _queryCollection[type].Parameters = parameters;
            DataBase.SendQuery(_queryCollection[type]);
        }
    }
}
