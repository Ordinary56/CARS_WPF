using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS_WPF.Helpers
{
    public static class ReaderRow
    {
        public static string AsString(DbDataReader reader)
        {
            StringBuilder stringBuilder = new();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                stringBuilder.Append(reader[i].ToString());
                if (i < reader.FieldCount - 1)
                    stringBuilder.Append(";");
            }
            return stringBuilder.ToString();
        }
    }
}
