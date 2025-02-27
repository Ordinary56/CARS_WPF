using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS_WPF.Model
{
    public sealed record Customer
    {
        public string CustomerName { get; init; }
        public string Phone { get; init; }
        public string City { get; init; }
        public Customer(string[] line)
        {
            CustomerName = line[0];
            Phone = line[1];
            City = line[2];
        }
    }
}
