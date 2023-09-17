using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.DTO
{
    public class ProductDetailsDTO
    {
        public string name { get; set; }
        public long quantity { get; set; }
        public string size { get; set; }
        public double pricePerUnit { get; set; }
        public double totalPrice { get; set; }
    }
}
