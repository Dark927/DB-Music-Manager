using MusicManager.DBManagement;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MusicManager.ViewModel
{
    public class MainViewModel
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["MusicManager.Properties.Settings.MusicManagerDBConnectionString"].ConnectionString;

        private DBToolsManager _dbToolManager;

        public ICommand DeleteCommand { get; }

        public DataTable AllMusicData { get; set; }
        public DataTable AuthorData { get; set; }

        public int SelectedAuthorId { get; set; }


        public MainViewModel()
        {
            _dbToolManager = new DBToolsManager(_connectionString);
            AllMusicData = _dbToolManager.RequestData(MusicDataType.All);
            //AuthorData = _dbToolManager.RequestData(AuthorDataType.All);
        }

        //public void RequestListUpdateByType(MainDataTypes type, params int[] parameters)
        //{
        //    DataTable updatedData = _dbDataProvider.RequestData(type, parameters);
        //    _mainWindow.UpdateInfoList(updatedData, type);
        //}

        //public bool IsDataAvailableOfType(MainDataTypes type, params int[] parameters)
        //{
        //    return _dbDataProvider.IsDataAvailable(type, parameters);
        //}

        //public void DeleteDataOfType(MainDataTypes type, params int[] parameters)
        //{
        //    _dbDataEditor.DeleteData(type, parameters);
        //}

        //public void Initialize()
        //{
        //    RequestListUpdateByType(MainDataTypes.Author);
        //    RequestListUpdateByType(MainDataTypes.AllMusic);
        //}
    }
}
