using MusicManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicManager.Model
{
    static class DataFilters
    {
        public static string GetMusicFilterByTitle(string title)
        {
            return $"Title = '{title}'";
        }

        public static string GetAuthorsFilterByName(string name)
        {
            return $"Name = '{name}'";
        }

        public static string GetAuthorMusicFilterById(string musicId)
        {
            return $"MusicId = '{musicId}'";
        }

        public static Func<string, string> GetDefaultFilteringMethod(MainDataTypes type)
        {
            return type switch
            {
                MainDataTypes.Author => GetAuthorsFilterByName,
                MainDataTypes.AllMusic => GetMusicFilterByTitle,
                MainDataTypes.AuthorMusic => GetAuthorMusicFilterById,

                _ => throw new NotImplementedException()
            };
        }
    }
}
