using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS_WPF.Repositories
{
    public abstract class RepositoryBase
    {
        private const string CONNECTION_STRING = "Server=localhost;Database=classicmodels;Uid=root;Pwd=;";
        protected MySqlConnection GetConnection()
        {
            return new MySqlConnection(CONNECTION_STRING);
        }
    }
}
