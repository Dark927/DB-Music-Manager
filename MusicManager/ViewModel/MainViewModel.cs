using MusicManager.DBManagement;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Windows;

namespace MusicManager
{
    public class MainViewModel
    {
        private MainWindow _mainWindow;
        private string _connectionString = ConfigurationManager.ConnectionStrings["MusicManager.Properties.Settings.MusicManagerDBConnectionString"].ConnectionString;
        private DBDataProviderBase<ContentListType> _dbDataProvider;

        public MainViewModel()
        {
            _mainWindow = new MainWindow(this);
            Application.Current.MainWindow = _mainWindow;
            _dbDataProvider = new MusicDataProvider(_connectionString);
        }

        public void SetActualAuthorMusicList(int authorID)
        {
            DataTable musicTable = _dbDataProvider.RequestData(ContentListType.AuthorMusic, authorID);
            _mainWindow.UpdateInfoList(musicTable, ContentListType.AuthorMusic);
        }

        public void Initialize()
        {
            DataTable authorTable = _dbDataProvider.RequestData(ContentListType.Author);
            DataTable allMusicTable = _dbDataProvider.RequestData(ContentListType.AllMusic);

            _mainWindow.UpdateInfoList(authorTable, ContentListType.Author);
            _mainWindow.UpdateInfoList(allMusicTable, ContentListType.AllMusic);

            _mainWindow.Show();
        }
    }
}
