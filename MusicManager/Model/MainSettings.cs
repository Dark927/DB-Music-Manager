using MusicManager.Utilities;
using System;
using Utility;

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
            _settingsData = new SettingsData() { ConnectionString = DBEncrypt.Encrypt() };
        }

        public void SaveStateToJson()
        {
            JsonDataManager.SaveDataToJson(_settingsData, JsonDataManager.DefaultSavePathDict[SaveDataType.Settings]);
        }
    }
}
