using MusicManager.DBManagement;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace MusicManager.ViewModel
{
    public enum MainDataTypes
    {
        Author,
        AuthorMusic,
        AllMusic,
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        private DBToolsManager _dbToolManager;

        private int _selectedAuthorId = 0;
        private int _selectedMusicId = 0;
        private int _selectedAuthorMusicId = 0;

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
                _allMusicData = value;
                OnPropertyChanged(nameof(AllMusicData));
            }
        }
        public DataTable AuthorData
        {
            get => _authorData;
            set
            {
                _authorData = value;
                OnPropertyChanged(nameof(AuthorData));
            }
        }

        public DataTable AuthorMusicData
        {
            get => _authorMusicData;
            set
            {
                _authorMusicData = value;
                OnPropertyChanged(nameof(AuthorMusicData));
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
                    UpdateAuthorMusicData(_selectedAuthorId);
                }
                OnPropertyChanged(nameof(SelectedAuthorId));
            }
        }

        public int SelectedMusicId
        {
            get => _selectedMusicId;
            set
            {
                if (value != _selectedMusicId)
                {
                    _selectedMusicId = value;
                }
                OnPropertyChanged(nameof(SelectedMusicId));
            }
        }

        #endregion

        public MainViewModel()
        {
            try
            {
                // Data init 

                App currentApplication = App.Current as App;

                _dbToolManager = new DBToolsManager(currentApplication.RequestDBConnectionString());
                UpdateUI();

                // Commands init 

                DeleteCommand = new RelayCommand(DeleteData, CanDeleteData);

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

        private void UpdateUI()
        {
            AuthorData = RequestRelevantData(AuthorDataType.Default);
            AllMusicData = RequestRelevantData(MusicDataType.Default);
            UpdateAuthorMusicData(SelectedAuthorId);

            OnPropertyChanged(nameof(SelectedAuthorId));
            OnPropertyChanged(nameof(SelectedMusicId));
        }

        private DataTable RequestRelevantData<T>(T dataType, params int[] parameters) where T : Enum
        {
            return _dbToolManager.RequestData(dataType, parameters);
        }

        private void DeleteData(object parameter)
        {
            MainDataTypes type = (MainDataTypes)parameter;

            switch (type)
            {
                case MainDataTypes.AllMusic:
                    _dbToolManager.DeleteData(MusicDataType.Default, SelectedMusicId);
                    break;
            }
            UpdateUI();
        }

        private bool CanDeleteData(object parameter)
        {
            return (parameter is MainDataTypes);
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
