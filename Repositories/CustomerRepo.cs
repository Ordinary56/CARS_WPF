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
    public sealed class CustomerRepo : RepositoryBase
    {
        public async IAsyncEnumerable<Customer> GetCustomersByCountry(string countryName)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();
            using var cmd = new MySqlCommand()
            {
                CommandText = "SELECT customerName, phone, city FROM CUSTOMERS WHERE country=@country;",
                Connection = conn,
            };
            cmd.Parameters.AddWithValue("@country", countryName);
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync()) 
            {
                string[] row = ReaderRow.AsString(reader).Split(';');
                yield return new Customer(row);
            }
        }
    }
}
