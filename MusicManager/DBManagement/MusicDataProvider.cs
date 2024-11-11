using System;
using System.Data;
using System.Linq;
using System.Windows;

namespace MusicManager.DBManagement
{
    internal class MusicDataProvider : DBDataProviderBase<ContentListType>
    {
        public MusicDataProvider(string connectionString) : base(connectionString) { }

        public override DataTable RequestData(ContentListType type, params int[] parameters)
        {
            return type switch
            {
                ContentListType.Author => RequestAuthors(),
                ContentListType.AuthorMusic => RequestSelectedAuthorMusic(parameters.FirstOrDefault(0)),
                ContentListType.AllMusic => RequestAllMusic(),

                _ => throw new NotSupportedException(),
            };

        }

        private DataTable RequestAuthors()
        {
            string authorQuery = "SELECT * FROM Author";
            return DataBaseManager.CreateAndSendQuery(authorQuery);
        }

        private DataTable RequestSelectedAuthorMusic(int authorID)
        {
            string musicQuery = "SELECT * FROM Music m inner join AuthorMusic am on m.Id = am.Id where am.AuthorId = @authorID;";
            return DataBaseManager.CreateAndSendQuery(musicQuery, authorID);
        }

        private DataTable RequestAllMusic()
        {
            string allMusicQuery = "SELECT * FROM Music";
            return DataBaseManager.CreateAndSendQuery(allMusicQuery);
        }
    }
}
