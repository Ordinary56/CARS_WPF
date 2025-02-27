using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS_WPF.Model
{
    public sealed record Product
    {
        public  string ProductName { get;  }       
        public string? ProductCode { get; }
        public decimal? BuyPrice { get; init; } = default;
        public Product(string[] line)
        {
            ProductName = line[0];
            if (line[1].StartsWith('S'))
                ProductCode = line[1];
            else 
                BuyPrice = decimal.Parse(line[1]);  
        }
        public override string ToString()
        {
            return $"{ProductName} {ProductCode} {BuyPrice}";
        }
    }
}
