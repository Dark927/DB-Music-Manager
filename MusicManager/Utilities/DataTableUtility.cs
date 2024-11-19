using System.Data;
using System.Linq;

namespace MusicManager.Utilities
{
    static class DataTableUtility
    {
        public static bool CheckElementExistsByFilter(this DataTable table, string filter)
        {
            DataRow[] rows = table.Select(filter);

            return rows.Length > 0;
        }

        public static DataRow GetFirstElementByFilter(this DataTable table, string filter)
        {
            return table.Select(filter).FirstOrDefault();
        }
    }
}
