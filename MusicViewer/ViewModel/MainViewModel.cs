using Microsoft.Win32;
using MusicManager.ViewModel;
using MusicViewer.DBManagement;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace MusicViewer
{
    public enum TableType
    {
        Author,
        Music,
        AuthorMusic
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        #region Fields

        private MusicFilesController _musicFilesController;

        public event PropertyChangedEventHandler PropertyChanged;

        private MusicDataClassesDataContext _musicDataClassesDataContext;

        private ObservableCollection<Author> _authorsList;
        private ObservableCollection<Music> _musicList;
        private ObservableCollection<AuthorMusic> _authorMusicList;

        private Table<Music> _musicTable;
        private Table<Author> _authorTable;
        private Table<AuthorMusic> _authorMusicTable;

        private object _activeTable;
        private object _selectedGridItem;
        private TableType _activeTableType;

        #endregion


        #region Properties

        public ICommand InsertCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand DownloadCommand { get; private set; }
        public ICommand UploadCommand { get; private set; }
        public ICommand DeleteMusicCommand { get; private set; }

        public ObservableCollection<TableType> TableTypes { get; set; }

        public MusicDataClassesDataContext ClassesDataContext
        {
            get => _musicDataClassesDataContext;
            private set => _musicDataClassesDataContext = value;
        }

        public ObservableCollection<Author> AuthorsList
        {
            get => _authorsList;
            set
            {
                _authorsList = value;
                OnPropertyChanged(nameof(AuthorsList));
            }
        }

        public ObservableCollection<AuthorMusic> AuthorMusicList
        {
            get => _authorMusicList;
            set
            {
                _authorMusicList = value;
                OnPropertyChanged(nameof(AuthorMusicList));
            }
        }

        public ObservableCollection<Music> MusicsList
        {
            get => _musicList;
            set
            {
                _musicList = value;
                OnPropertyChanged(nameof(MusicsList));
            }
        }

        public TableType ActiveTableType
        {
            get => _activeTableType;
            set
            {
                _activeTableType = value;
                SwitchActiveDataList(value);
                OnPropertyChanged(nameof(ActiveTableType));
            }
        }

        public object ActiveTable
        {
            get => _activeTable;
            set
            {
                _activeTable = value;
                OnPropertyChanged(nameof(ActiveTable));
            }
        }

        public object SelectedGridItem
        {
            get => _selectedGridItem;
            set
            {
                _selectedGridItem = value;
                OnPropertyChanged(nameof(SelectedGridItem));
            }
        }

        #endregion


        #region Methods 

        public MainViewModel()
        {
            string connection = "Server=tcp:musicmanagerserver.database.windows.net,1433;Initial Catalog=MusicDB;Persist Security Info=False;User ID=dark;Password=u4AwD–s_\\$KHy}Y;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            ClassesDataContext = new MusicDataClassesDataContext(connection);


            TableTypes = new ObservableCollection<TableType>((TableType[])Enum.GetValues(typeof(TableType)));

            InitTables();
            SyncDataWithDB();


            ActiveTable = MusicsList;
            ActiveTableType = TableType.Music;
            _musicFilesController = new MusicFilesController(MusicsList);

            InitCommands();
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void InitCommands()
        {
            InsertCommand = new RelayCommand(Insert);
            SaveCommand = new RelayCommand(SaveChanges);
            CancelCommand = new RelayCommand(CancelChanges);
            DownloadCommand = new RelayCommand(_musicFilesController.RequestFile);
            UploadCommand = new RelayCommand(_musicFilesController.PushFile);
            DeleteMusicCommand = new RelayCommand(_musicFilesController.DeleteFile);
        }

        private void InitTables()
        {
            _musicTable = ClassesDataContext.Musics;
            _authorTable = ClassesDataContext.Authors;
            _authorMusicTable = ClassesDataContext.AuthorMusics;
        }

        private void SyncDataWithDB()
        {
            MusicsList = new ObservableCollection<Music>(_musicTable.ToList());
            AuthorsList = new ObservableCollection<Author>(_authorTable.ToList());
            AuthorMusicList = new ObservableCollection<AuthorMusic>(_authorMusicTable.ToList());
            SwitchActiveDataList(ActiveTableType);
        }

        private void Insert(object parameter)
        {
            MusicsList.Add(new Music() { Id = MusicsList.Last().Id + 1, Title = "505", Duration = "", Style = "" });
        }

        private void SaveChanges(object parameter)
        {
            UpdateDB(_musicTable, MusicsList, DataComparators.MusicComparator);

            ClassesDataContext.SubmitChanges();
            SyncDataWithDB();
        }

        private void UpdateDB<T>(Table<T> table, ObservableCollection<T> newData, Func<T, T, bool> comparator) where T : class
        {
            var existingDataList = table.ToList();

            foreach (var data in newData)
            {
                bool exists = existingDataList.Any(existingData => comparator(existingData, data));

                if (!exists)
                {
                    table.InsertOnSubmit(data);
                    existingDataList.Add(data);
                }
            }
        }

        private void CancelChanges(object parameter)
        {
            ClassesDataContext.Refresh(RefreshMode.OverwriteCurrentValues, ClassesDataContext.Musics);
            ClassesDataContext.Refresh(RefreshMode.OverwriteCurrentValues, ClassesDataContext.AuthorMusics);
            ClassesDataContext.Refresh(RefreshMode.OverwriteCurrentValues, ClassesDataContext.Authors);
            SyncDataWithDB();
        }

        private void SwitchActiveDataList(TableType type)
        {
            object targetDataList = null;

            switch (type)
            {
                case TableType.Music:
                    targetDataList = MusicsList;
                    break;

                case TableType.Author:
                    targetDataList = AuthorsList;
                    break;

                case TableType.AuthorMusic:
                    targetDataList = AuthorMusicList;
                    break;
            };


            if (targetDataList != null)
            {
                ActiveTable = targetDataList;
            }
        }

        #endregion
    }
}
