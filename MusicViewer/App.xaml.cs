using MusicViewer.Scripts;
using System.Windows;

namespace MusicViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            string connectionString = "Server=tcp:musicmanagerserver.database.windows.net,1433;Initial Catalog=MusicDB;Persist Security Info=False;User ID=dark;Password=u4AwD–s_\\$KHy}Y;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            DBDataContext.Instance.InitDataContext(connectionString);

            if (!DBDataContext.Instance.GetContext().DatabaseExists())
            {
                MessageBox.Show($"{this.ToString()} : Data base can not be used. Data context not correct!\n # Check the connection string!");
            }
        }
    }
}
