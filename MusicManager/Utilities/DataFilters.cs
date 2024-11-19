using MusicManager.DBManagement;
using System;

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

        public static string GetFilterById(string id)
        {
            return $"Id = '{id}'";
        }

        public static Func<string, string> GetDefaultFilteringMethod<T>(T type) where T : Type
        {
            return type switch
            {
                { } when type == typeof(AuthorDataType) => GetAuthorsFilterByName,
                { } when type == typeof(MusicDataType) => GetMusicFilterByTitle,
                { } when type == typeof(AuthorMusicDataType) => GetAuthorMusicFilterById,
                _ => throw new NotImplementedException()
            };
        }
    }
}
