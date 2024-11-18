using MusicManager.DBManagement;
using MusicManager.DBManagement.ManagementKits;
using MusicManager.Model;
using MusicManager.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
        // -------------------------------------------------------------------
        // Fields
        // -------------------------------------------------------------------

        #region Fields 

        private DBToolsManager _dbToolManager;

        private int _selectedAuthorId = 0;
        private int _selectedMusicId = 0;
        private int _selectedAuthorMusicId = 0;

        private DataTable _allMusicData;
        private DataTable _authorMusicData;
        private DataTable _authorData;

        private string _textBoxInput;

        private Dictionary<MainDataTypes, string> _mainInfoColumnsDict = new Dictionary<MainDataTypes, string>
        {
            [MainDataTypes.AllMusic] = "Title",
            [MainDataTypes.AuthorMusic] = "Title",
            [MainDataTypes.Author] = "Name",
        };

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion


        // -------------------------------------------------------------------
        // Properties
        // -------------------------------------------------------------------

        #region Properties

        public ICommand DeleteCommand { get; private set; }
        public ICommand AddCommand { get; private set; }

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
                DisplaySelectedItemToTB(AuthorData, SelectedAuthorId, MainDataTypes.Author);
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
                DisplaySelectedItemToTB(AllMusicData, SelectedMusicId, MainDataTypes.AllMusic);
                OnPropertyChanged(nameof(SelectedMusicId));
            }
        }

        public int SelectedAuthorMusicId
        {
            get => _selectedAuthorMusicId;
            set
            {
                if (value != _selectedAuthorMusicId)
                {
                    _selectedAuthorMusicId = value;
                }
                DisplaySelectedItemToTB(AuthorMusicData, SelectedAuthorMusicId, MainDataTypes.AuthorMusic);
                OnPropertyChanged(nameof(SelectedAuthorMusicId));
            }
        }

        public string TextBoxInput
        {
            get => _textBoxInput;
            set
            {
                if (value != _textBoxInput)
                {
                    _textBoxInput = value;
                }
                OnPropertyChanged(nameof(TextBoxInput));
            }
        }

        #endregion


        // -------------------------------------------------------------------
        // Methods
        // -------------------------------------------------------------------

        #region Methods

        public MainViewModel()
        {
            try
            {
                ListBox newLs = new ListBox();
                App currentApplication = App.Current as App;
                _dbToolManager = new DBToolsManager(currentApplication.RequestDBConnectionString());

                UpdateUI();
                InitCommands();
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

        private void InitCommands()
        {
            DeleteCommand = new RelayCommand(DeleteData, CanUpdateData);
            AddCommand = new RelayCommand(AddData, CanUpdateData);
        }

        private void UpdateAuthorMusicData(int selectedAuthorIndex)
        {
            AuthorMusicData = RequestRelevantData(AuthorMusicDataType.Default, selectedAuthorIndex.ToString());
        }

        private void UpdateUI()
        {
            AuthorData = RequestRelevantData(AuthorDataType.Default);
            AllMusicData = RequestRelevantData(MusicDataType.Default);
            UpdateAuthorMusicData(SelectedAuthorId);

            OnPropertyChanged(nameof(SelectedAuthorId));
            OnPropertyChanged(nameof(SelectedMusicId));
        }

        private DataTable RequestRelevantData<T>(T dataType, params string[] parameters) where T : Enum
        {
            return _dbToolManager.RequestData(dataType, parameters);
        }

        private void DeleteData(object parameter)
        {
            MainDataTypes type = (MainDataTypes)parameter;

            switch (type)
            {
                case MainDataTypes.AllMusic:
                    _dbToolManager.UpdateData(ToolType.DataRemover, MusicDataType.Default, SelectedMusicId.ToString());
                    SelectedMusicId = 0;
                    break;

                case MainDataTypes.Author:
                    _dbToolManager.UpdateData(ToolType.DataRemover, AuthorDataType.Default, SelectedAuthorId.ToString());
                    SelectedAuthorId = 0;
                    break;

                case MainDataTypes.AuthorMusic:
                    _dbToolManager.UpdateData(ToolType.DataRemover, AuthorMusicDataType.Default, SelectedAuthorMusicId.ToString());
                    SelectedAuthorMusicId = 0;
                    break;
            }
            UpdateUI();
        }

        private void AddData(object parameter)
        {
            MainDataTypes type = (MainDataTypes)parameter;

            switch (type)
            {
                case MainDataTypes.AllMusic:
                    if (CanAddData(MainDataTypes.AllMusic, AllMusicData, TextBoxInput))
                    {
                        _dbToolManager.UpdateData(ToolType.DataAdder, MusicDataType.Default, TextBoxInput, "-", "-");
                    }
                    break;

                case MainDataTypes.Author:
                    if (CanAddData(MainDataTypes.Author, AuthorData, TextBoxInput))
                    {
                        _dbToolManager.UpdateData(ToolType.DataAdder, AuthorDataType.Default, TextBoxInput);
                    }
                    break;

                case MainDataTypes.AuthorMusic:
                    if (CanAddData(MainDataTypes.AuthorMusic, AuthorMusicData, SelectedMusicId.ToString()))
                    {
                        _dbToolManager.UpdateData(ToolType.DataAdder, AuthorMusicDataType.Default, SelectedAuthorId.ToString(), SelectedMusicId.ToString());
                    }
                    break;
            }
            UpdateUI();
        }

        private bool CanAddData(MainDataTypes type, DataTable table, string uniqueInfo)
        {
            var dataFilteringMethod = DataFilters.GetDefaultFilteringMethod(type);
            string filter = dataFilteringMethod.Invoke(uniqueInfo);
            return !table.CheckElementExistsByFilter(filter);
        }

        private bool CanUpdateData(object parameter)
        {
            return (parameter is MainDataTypes);
        }

        private void DisplaySelectedItemToTB(DataTable table, int selectedId, MainDataTypes type)
        {
            DataRow dataRow = table.GetFirstElementByFilter(DataFilters.GetFilterById(selectedId.ToString()));

            string mainValueName = _mainInfoColumnsDict[type];

            if (dataRow != null)
            {
                string selectedValue = dataRow.Field<string>(mainValueName);
                TextBoxInput = selectedValue;
            }
        }

        #endregion
    }
}
