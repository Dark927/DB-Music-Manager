using MusicManager.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MusicManager.Model
{
    internal class MainSettings
    {
        public EventHandler OnPropertyChanged;
        private SettingsData _settingsData;

        public string ConnectionString { get => _settingsData.ConnectionString; }

        public MainSettings()
        {
            LoadStateFromJson();
        }

        public void LoadStateFromJson(string path = null)
        {
            string loadingPath = path ?? JsonDataManager.DefaultSavePathDict[SaveDataType.Settings];
            _settingsData = JsonDataManager.LoadObjectFromJson<SettingsData>(loadingPath);

            if (_settingsData == null)
            {
                throw new NullReferenceException($"{nameof(_settingsData)} is null, can not connect to the DB!");
            }
        }

        public void SaveStateToJson()
        {
            JsonDataManager.SaveDataToJson(_settingsData, JsonDataManager.DefaultSavePathDict[SaveDataType.Settings]);
        }
    }
}
