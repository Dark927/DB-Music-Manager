using MusicManager.Model;
using System.Windows;

namespace MusicManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainSettings _settings;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _settings = new MainSettings();
        }

        public string RequestDBConnectionString()
        {
            return _settings?.ConnectionString;
        }

    }
}
