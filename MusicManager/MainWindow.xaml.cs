using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.ComponentModel.DataAnnotations;
using MusicManager.ViewModel;

namespace MusicManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public enum MainDataTypes
    {
        Author,
        AuthorMusic,
        AllMusic,
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
