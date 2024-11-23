using System.Data.Linq;
using System.Windows;

namespace MusicViewer.Scripts
{
    public static class InfoBoxes
    {
        public static void ContextStatusInfo(DataContext dataContext)
        {
            if (DBDataContext.HasContextChanges(dataContext))
            {
                ContextChangesInfo(dataContext);
            }
            else
            {
                DataUpToDateInfo();
            }
        }

        public static void DataUpToDateInfo()
        {
            MessageBox.Show("Data status : Up To Date", "Info");
        }

        public static void ContextChangesInfo(DataContext dataContext)
        {
            var changeSet = dataContext.GetChangeSet();
            string changesInfo =
                $"Inserts \t:    {changeSet.Inserts.Count}\n" +
                $"Deletes \t:    {changeSet.Deletes.Count}\n" +
                $"Updates \t:    {changeSet.Updates.Count}";

            MessageBox.Show(changesInfo, "Changes Info");
        }
    }
}
