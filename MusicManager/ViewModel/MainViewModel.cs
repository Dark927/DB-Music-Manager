using MusicManager.DBManagement;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace MusicManager
{
    public class MainViewModel
    {
        private MainWindow _mainWindow;
        private string _connectionString = ConfigurationManager.ConnectionStrings["MusicManager.Properties.Settings.MusicManagerDBConnectionString"].ConnectionString;
        private DBDataProviderBase<DataListType> _dbDataProvider;
        private DBDataEditorBase<DataListType> _dbDataEditor;

        public MainViewModel()
        {
            _mainWindow = new MainWindow(this);
            Application.Current.MainWindow = _mainWindow;

            _dbDataProvider = new MusicDataProvider(_connectionString);
            _dbDataEditor = new MusicDataEditor(_connectionString);
        }

        public void RequestListUpdateByType(DataListType type, params int[] parameters)
        {
            DataTable updatedData = _dbDataProvider.RequestData(type, parameters);
            _mainWindow.UpdateInfoList(updatedData, type);
        }

        public bool IsDataAvailableOfType(DataListType type, params int[] parameters)
        {
            return _dbDataProvider.IsDataAvailable(type, parameters);
        }

        public void DeleteDataOfType(DataListType type, params int[] parameters)
        {
            _dbDataEditor.DeleteData(type, parameters);
        }

        public void Initialize()
        {
            RequestListUpdateByType(DataListType.Author);
            RequestListUpdateByType(DataListType.AllMusic);

            _mainWindow.Show();
        }
    }
}
