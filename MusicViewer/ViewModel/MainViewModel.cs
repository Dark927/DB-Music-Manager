using MusicManager.ViewModel;
using MusicViewer.DBManagement;
using MusicViewer.Interface;
using MusicViewer.Scripts;
using MusicViewer.Scripts.Audio;
using MusicViewer.ViewModel;
using NAudio.Wave;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Linq;
using System.Linq;
using System.Windows.Input;

namespace MusicViewer
{
    public enum TableType
    {
        Author,
        Music,
        AuthorMusic
    }

    public class MainViewModel : INotifyPropertyChanged, IViewModel
    {
        #region Fields

        private MusicFilesController _musicFilesController;

        public event PropertyChangedEventHandler PropertyChanged;

        private MusicDataClassesDataContext _dataContext;
        private DBUpdater _dbUpdater;

        private LocalDataModel _localDataManager;

        private object _activeTable;
        private object _selectedGridItem;
        private TableType _activeTableType;

        private double _sliderMaxValue;
        private double _sliderValue;

        private WaveStream _activeAudioWave;


        #endregion


        #region Properties

        public ICommand InsertCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand DownloadCommand { get; private set; }
        public ICommand UploadCommand { get; private set; }
        public ICommand DeleteMusicCommand { get; private set; }
        public ICommand PlayMusicCommand { get; private set; }
        public ICommand StopMusicCommand { get; private set; }

        public ObservableCollection<TableType> TableTypes { get; set; }

        public MusicDataClassesDataContext ClassesDataContext
        {
            get => _dataContext;
            private set => _dataContext = value;
        }

        public ObservableCollection<Author> AuthorsList
        {
            get => _localDataManager.Authors;
            set
            {
                _localDataManager.Authors = value;
                OnPropertyChanged(nameof(AuthorsList));
            }
        }

        public ObservableCollection<AuthorMusic> AuthorMusicList
        {
            get => _localDataManager.AuthorMusic;
            set
            {
                _localDataManager.AuthorMusic = value;
                OnPropertyChanged(nameof(AuthorMusicList));
            }
        }

        public ObservableCollection<Music> MusicsList
        {
            get => _localDataManager.Musics;
            set
            {
                _localDataManager.Musics = value;
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

        public double SliderMaxValue
        {
            get => _sliderMaxValue;
            set
            {
                if (value != _sliderMaxValue)
                {
                    _sliderMaxValue = value;
                    OnPropertyChanged(nameof(SliderMaxValue));
                }
            }
        }

        public double SliderValue
        {
            get => _sliderValue;
            set
            {
                if (value != _sliderValue)
                {
                    _sliderValue = value;
                    OnPropertyChanged(nameof(SliderValue));
                }
            }
        }

        #endregion


        #region Methods 

        public MainViewModel()
        {
            InitDataAndSync();
            InitCommands();
            SetDefaultProperties();
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SyncDataWithDB(MusicDataClassesDataContext context)
        {
            MusicsList = new ObservableCollection<Music>(context.Musics.ToList());
            AuthorsList = new ObservableCollection<Author>(context.Authors.ToList());
            AuthorMusicList = new ObservableCollection<AuthorMusic>(context.AuthorMusics.ToList());
            SwitchActiveDataList(ActiveTableType);
        }

        private void SyncViewModel(object sender, EventArgs e)
        {
            SyncDataWithDB(ClassesDataContext);
        }

        private void InitDataAndSync()
        {
            TableTypes = new ObservableCollection<TableType>((TableType[])Enum.GetValues(typeof(TableType)));

            ClassesDataContext = DBDataContext.Instance.GetContext();
            _musicFilesController = new MusicFilesController(ClassesDataContext);
            _localDataManager = new LocalDataModel();

            _dbUpdater = new DBUpdater(ClassesDataContext);
            _dbUpdater.OnDataUpdated += SyncViewModel;

            SyncDataWithDB(ClassesDataContext);

            ActiveTable = MusicsList;
            ActiveTableType = TableType.Music;
        }

        private void SetDefaultProperties()
        {
            SliderValue = 0;
            SliderMaxValue = 100;
        }

        private void InitCommands()
        {
            InsertCommand = new RelayCommand(InsertSong);
            SaveCommand = new RelayCommand(SaveChanges);
            CancelCommand = new RelayCommand(CancelChanges);
            DownloadCommand = new RelayCommand(_musicFilesController.RequestFile);
            UploadCommand = new RelayCommand(_musicFilesController.PushFile);
            DeleteMusicCommand = new RelayCommand(_musicFilesController.DeleteFile);
            PlayMusicCommand = new RelayCommand(PlaySong);
            StopMusicCommand = new RelayCommand(StopSong);
        }

        private void PlaySong(object parameter)
        {
            _musicFilesController.PlaySong(parameter);

            IAudioPlayer audioPlayer = _musicFilesController.ActiveAudioPlayer;
            audioPlayer.OnAudioLoaded += AudioLoaded;
            audioPlayer.OnTimerTick += SliderUpdate;

        }

        private void StopSong(object parameter)
        {
            IAudioPlayer audioPlayer = _musicFilesController.ActiveAudioPlayer;

            if (audioPlayer != null)
            {
                audioPlayer.Stop();
                SliderValue = 0;
            }
        }

        private void AudioLoaded(object sender, AudioArgs e)
        {
            SliderMaxValue = e.TotalSeconds;
        }

        private void SliderUpdate(object sender, AudioArgs e)
        {
            SliderValue = e.TotalSeconds;
        }

        private void InsertSong(object parameter)
        {
            MusicsList.Add(new Music() { Id = MusicsList.Last().Id + 1, Title = "505", Duration = "", Style = "" });
        }

        private void SaveChanges(object parameter)
        {
            UpdateDB(_dataContext.Musics, MusicsList, DataComparators.MusicComparator);


            if (DBDataContext.HasContextChanges(ClassesDataContext))
            {
                InfoBoxes.ContextChangesInfo(ClassesDataContext);
                ClassesDataContext.SubmitChanges();
                SyncDataWithDB(ClassesDataContext);
            }
            else
            {
                InfoBoxes.DataUpToDateInfo();
            }
        }


        private void UpdateDB<T>(Table<T> table, ObservableCollection<T> newData, Func<T, T, bool> comparator) where T : class
        {
            var existingDataList = table.ToList();

            // Remove data which not exists in Table

            var itemsToRemove = existingDataList
                .Where(existingItem => !newData.Any(data => comparator(existingItem, data)))
                .ToList();

            table.DeleteAllOnSubmit(itemsToRemove);

            // Add data which not exists in Table

            var itemsToInsert = newData
                .Where(data => !existingDataList.Any(existingData => comparator(data, existingData)))
                .Distinct()
                .ToList();

            table.InsertAllOnSubmit(itemsToInsert);
        }

        private void CancelChanges(object parameter)
        {
            _dbUpdater.CancelChanges(this, parameter);
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
