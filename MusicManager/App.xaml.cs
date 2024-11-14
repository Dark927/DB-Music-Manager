using MusicManager.Model;
using MusicManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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

            try
            {
                _settings.LoadStateFromJson();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string RequestDBConnectionString()
        {
            return _settings?.ConnectionString;
        }

    }
}
