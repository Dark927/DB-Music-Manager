﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicManager.Utilities
{
    static class DataTableUtility
    {
        public static bool CheckElementExistsByFilter(DataTable table, string filter)
        {
            DataRow[] rows = table.Select(filter);

            return rows.Length > 0;
        }
    }
}