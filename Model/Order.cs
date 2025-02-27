using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS_WPF.Model
{
    public sealed record Order
    {
        public int OrderNumber { get; }
        public DateTime OrderDate { get; }
        public DateTime RequiredDate { get; }
        public DateTime? ShippedDate { get; }
        public string Status { get; }
        public string? Comments { get; }
        public int CustomerNumber { get; }

        public Order(string[] line)
        {
            OrderNumber = int.Parse(line[0]);
            OrderDate = DateTime.Parse(line[1]);
            RequiredDate = DateTime.Parse(line[2]);
            ShippedDate = DateTime.TryParse(line[3], out var dt) ? dt : null;
            Status = line[4];
            Comments = line[5];
            CustomerNumber = int.TryParse(line[^1], out int res) ? res : 0;
        }

    }
}
