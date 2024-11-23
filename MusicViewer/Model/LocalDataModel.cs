using System.Collections.ObjectModel;

namespace MusicViewer.ViewModel
{
    internal class LocalDataModel
    {
        private ObservableCollection<Music> _musicList;
        private ObservableCollection<Author> _authorsList;
        private ObservableCollection<AuthorMusic> _authorMusicList;

        public ObservableCollection<Music> Musics { get => _musicList; set => _musicList = value; }
        public ObservableCollection<Author> Authors { get => _authorsList; set => _authorsList = value; }
        public ObservableCollection<AuthorMusic> AuthorMusic { get => _authorMusicList; set => _authorMusicList = value; }

        public LocalDataModel()
        {

        }
    }
}
