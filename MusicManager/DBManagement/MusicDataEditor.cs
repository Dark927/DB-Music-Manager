using MusicManager.DBManagement.Query;
using MusicManager.Utilities;
using System;
using System.IO;

namespace MusicManager.DBManagement
{
    internal class MusicDataEditor : DBDataEditorBase<MainDataTypes>
    {
        private string _statePath = Path.Combine(JsonDataManager.DefaultSavePathDict[SaveDataType.Query], "musicDataEditor.json");
        private DBQueryCollection<MainDataTypes> _queryCollection;

        public MusicDataEditor(DataBase dataBase) : base(dataBase)
        {
            _queryCollection = new DBQueryCollection<MainDataTypes>();
            _queryCollection.LoadStateFromJson(_statePath);
        }

        public override void AddData(MainDataTypes type, params int[] parameters)
        {
            _queryCollection[type].Parameters = parameters;
            DataBase.SendQuery(_queryCollection[type]);
        }

        public override bool TryAddData(MainDataTypes type, params int[] parameters)
        {
            bool isAdded = false;

            if (_queryCollection.ContainKey(type))
            {
                _queryCollection[type].Parameters = parameters;
                isAdded = DataBase.SendQuery(_queryCollection[type]).DefaultView.Count != 0;
            }

            return isAdded;
        }

        public override void DeleteData(MainDataTypes type, params int[] parameters)
        {
            _queryCollection[type].Parameters = parameters;
            DataBase.SendQuery(_queryCollection[type]);
        }
    }
}
