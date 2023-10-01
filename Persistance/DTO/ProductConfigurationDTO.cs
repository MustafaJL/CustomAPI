using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.DTO
{
    public class ProductConfigurationDTO
    {
        public string sizeId { get; set; }
        public long totalQuantity { get; set; }
        public double price { get; set; }
        public double discountPrice { get; set; }
    }
}
