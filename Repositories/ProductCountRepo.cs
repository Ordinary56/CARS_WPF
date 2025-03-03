using CARS_WPF.Helpers;
using CARS_WPF.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CARS_WPF.Repositories
{
    public sealed class ProductCountRepo : RepositoryBase
    {
        public async Task<int> GetProductCount(Product product)
        {
            if (product == null)
            {
                return 0;
            }
            var conn = GetConnection();
            await conn.OpenAsync();
            MySqlCommand cmd = new()
            {
                CommandText = "SELECT quantityOrdered FROM orderdetails WHERE productCode=@Code",
                Connection = conn
            };
            cmd.Parameters.AddWithValue("@Code", product.ProductCode);
            int orderCount =  Convert.ToInt32(await cmd.ExecuteScalarAsync());
            return orderCount;
        }
    }
}
