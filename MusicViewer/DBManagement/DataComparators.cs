using System;

namespace MusicViewer.DBManagement
{
    static public class DataComparators
    {
        public static Func<Music, Music, bool> MusicComparator = (first, second) => (first.Id == second.Id) || (first.Title == second.Title);
    }
}
