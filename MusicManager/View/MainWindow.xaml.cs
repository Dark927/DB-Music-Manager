using System.Windows;
using System.Windows.Controls;
using System.Data;

namespace MusicManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public enum ContentListType
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

        public void UpdateInfoList(DataTable data, ContentListType infoType)
        {
            ListBox targetListBox = GetListBoxByContentType(infoType);

            if (targetListBox != null)
            {
                targetListBox.ItemsSource = data.DefaultView;
            }
        }

        private ListBox GetListBoxByContentType(ContentListType type)
        {
            return type switch
            {
                ContentListType.Author => AuthorsListBox,
                ContentListType.AuthorMusic => AuthorMusicListBox,
                ContentListType.AllMusic => AllMusicListBox,
                _ => null
            };
        }

        private void MusicLb_SelectionChanged(object sender, RoutedEventArgs e)
        {
            _viewModel.SetActualAuthorMusicList((int)AuthorsListBox.SelectedValue);
        }

        private void DeleteAuthor_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
