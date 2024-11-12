using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace MusicManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public enum DataListType
    {
        Author,
        AuthorMusic,
        AllMusic,
    }

    public partial class MainWindow : Window
    {
        public ListBox AuthorMusicListBox { get => _authorMusicListBox; }
        public ListBox AuthorsListBox { get => _authorsListBox; }
        public ListBox AllMusicListBox { get => _allMusicListBox; }

        private MainViewModel _viewModel;

        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;

            AuthorsListBox.DisplayMemberPath = "Name";
            AuthorsListBox.SelectedValuePath = "Id";

            AuthorMusicListBox.DisplayMemberPath = "Music";
            AuthorMusicListBox.SelectedValuePath = "Id";

            AllMusicListBox.DisplayMemberPath = "Music";
            AllMusicListBox.SelectedValuePath = "Id";
        }

        public void UpdateInfoList(DataTable data, DataListType infoType)
        {
            ListBox targetListBox = GetListBoxByContentType(infoType);

            if (targetListBox != null)
            {
                targetListBox.ItemsSource = data.DefaultView;
            }
        }

        private ListBox GetListBoxByContentType(DataListType type)
        {
            return type switch
            {
                DataListType.Author => AuthorsListBox,
                DataListType.AuthorMusic => AuthorMusicListBox,
                DataListType.AllMusic => AllMusicListBox,
                _ => null
            };
        }

        private void MusicLb_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (AuthorsListBox.SelectedValue != null)
            {
                _viewModel.RequestListUpdateByType(DataListType.AuthorMusic, (int)AuthorsListBox.SelectedValue);
            }
            else
            {
                AuthorMusicListBox.ItemsSource = null;
            }
        }

        private void DeleteAuthor_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.DeleteDataOfType(DataListType.Author, (int)AuthorsListBox.SelectedValue);
        }

        private void UpdateAuthor_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.RequestListUpdateByType(DataListType.Author);
        }
    }
}
