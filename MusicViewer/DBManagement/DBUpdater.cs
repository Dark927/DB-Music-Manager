using MusicViewer.Interface;
using MusicViewer.Scripts;
using System;
using System.Data.Linq;

namespace MusicViewer.DBManagement
{
    public class DBUpdater
    {
        private MusicDataClassesDataContext _dataContext;

        public event EventHandler OnDataUpdated;

        public MusicDataClassesDataContext ClassesDataContext { get => _dataContext; }

        public DBUpdater(MusicDataClassesDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void CancelChanges(IViewModel sender, object parameter, bool showInfo = true)
        {
            if (DBDataContext.HasContextChanges(ClassesDataContext))
            {
                ClassesDataContext.Refresh(RefreshMode.OverwriteCurrentValues, ClassesDataContext.Musics);
                ClassesDataContext.Refresh(RefreshMode.OverwriteCurrentValues, ClassesDataContext.AuthorMusics);
                ClassesDataContext.Refresh(RefreshMode.OverwriteCurrentValues, ClassesDataContext.Authors);
                OnDataUpdated?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                if (showInfo)
                {
                    InfoBoxes.DataUpToDateInfo();
                }
            }
        }
    }
}
