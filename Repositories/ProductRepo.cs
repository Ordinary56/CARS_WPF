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
    public sealed class ProductRepo : RepositoryBase
    {
        public async IAsyncEnumerable<Product> GetProductsAsync()
        {
            using MySqlConnection conn = GetConnection();
            await conn.OpenAsync();
            MySqlCommand cmd = new()
            {
                CommandText = "SELECT productName, productCode FROM products",
                Connection = conn
            };
           using var reader = await cmd.ExecuteReaderAsync();
           while(await reader.ReadAsync())
           {
                var row = ReaderRow.AsString(reader).Split(';');
                yield return new Product(row);
           }
        }

        public async IAsyncEnumerable<Product> GetProductsByOrderAsync(Order order)
        {

            using MySqlConnection conn = GetConnection();
            await conn.OpenAsync();
            MySqlCommand cmd = new()
            {
                CommandText = "SELECT p.productName, p.buyPrice \r\nFROM products p\r\nINNER JOIN orderdetails od ON p.productCode = od.productCode\r\nINNER JOIN orders o ON od.orderNumber = o.orderNumber\r\nWHERE o.orderNumber = @orderNumber;",
                Connection = conn
            };
            cmd.Parameters.AddWithValue("@orderNumber", order.OrderNumber);
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync()) 
            {
                string[] row = ReaderRow.AsString(reader).Split(';');
                yield return new Product(row);
            }
        }
    }
}
