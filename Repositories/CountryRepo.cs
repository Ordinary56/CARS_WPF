using CARS_WPF.Helpers;
using CARS_WPF.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS_WPF.Repositories
{
    public sealed class CountryRepo : RepositoryBase
    {
        public async IAsyncEnumerable<Country> GetCountriesAsync()
        {
            using var conn = GetConnection();
            await conn.OpenAsync();
            using var cmd = new MySqlCommand()
            {
                CommandText = "SELECT DISTINCT country FROM customers ORDER BY country ASC",
                Connection = conn
            };
            using var reader = await cmd.ExecuteReaderAsync();
            while(await reader.ReadAsync())
            {
                string[] row = ReaderRow.AsString(reader).Split(';');
                yield return new Country { Name = row[0] };
            }
            
        }
    }
}
