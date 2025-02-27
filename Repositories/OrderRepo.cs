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
    public sealed class OrderRepo : RepositoryBase
    {
        public async IAsyncEnumerable<Order> GetOrdersAsync()
        {
            using var conn = GetConnection();
            await conn.OpenAsync();
            using var cmd = new MySqlCommand()
            {
                CommandText = "SELECT * FROM orders",
                Connection = conn
            };
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync()) 
            {
                string[] row = ReaderRow.AsString(reader).Split(';');
                yield return new Order(row);
            }
        }
    }
}
