using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MusicViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> _constantPropertiesNames;
        private List<string> _hiddenPropertiesNames;

        public MainWindow()
        {
            InitializeComponent();

            InitConstantProperties();
            InitHiddenPropertiesNames();
        }

        private void InitConstantProperties()
        {
            _constantPropertiesNames = new List<string>
            {
                "MusicFile",
                "FileID",
                "Id"
            };
        }

        private void InitHiddenPropertiesNames()
        {
            _hiddenPropertiesNames = new List<string>
            {
                "AuthorMusics"
            };
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            foreach (var property in _constantPropertiesNames)
            {
                if (property == e.PropertyName)
                {
                    e.Column.IsReadOnly = true;
                }
            }

            foreach (var property in _hiddenPropertiesNames)
            {
                if (e.PropertyName == property)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void Slider_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
