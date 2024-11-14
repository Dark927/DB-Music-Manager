using MusicManager.DBManagement;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace MusicManager.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private DBToolsManager _dbToolManager;
        private int _selectedAuthorId = 0;
        private DataTable _allMusicData;
        private DataTable _authorMusicData;
        private DataTable _authorData;

        public event PropertyChangedEventHandler PropertyChanged;

        #region Properties

        public ICommand DeleteCommand { get; }

        public DataTable AllMusicData
        {
            get => _allMusicData;
            set
            {
                if(_allMusicData != value)
                {
                    _allMusicData = value;
                    OnPropertyChanged(nameof(AllMusicData));
                }
            }
        }
        public DataTable AuthorData
        {
            get => _authorData;
            set
            {
                if (_authorData != value)
                {
                    _authorData = value;
                    OnPropertyChanged(nameof(AuthorData));
                }
            }
        }

        public DataTable AuthorMusicData
        {
            get => _authorMusicData;
            set
            {
                if (_authorMusicData != value)
                {
                    _authorMusicData = value;
                    OnPropertyChanged(nameof(AuthorMusicData));
                }
            }
        }

        public int SelectedAuthorId
        {
            get => _selectedAuthorId;
            set
            {
                if (value != _selectedAuthorId)
                {
                    _selectedAuthorId = value;
                    OnPropertyChanged(nameof(SelectedAuthorId));
                    UpdateAuthorMusicData(_selectedAuthorId);
                }
            }
        }

        #endregion

        public MainViewModel()
        {
            try
            {
                App currentApplication = App.Current as App;

                _dbToolManager = new DBToolsManager(currentApplication.RequestDBConnectionString());

                AuthorData = new DataTable();
                AuthorData = RequestRelevantData(AuthorDataType.Default);
                AllMusicData = RequestRelevantData(MusicDataType.Default);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateAuthorMusicData(int selectedAuthorIndex)
        {
            AuthorMusicData = RequestRelevantData(AuthorMusicDataType.Default, selectedAuthorIndex);
        }

        private DataTable RequestRelevantData<T>(T dataType, params int[] parameters) where T : Enum
        {
            return _dbToolManager.RequestData(dataType, parameters);
        }




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
