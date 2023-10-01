using Domain.Modals.SharedFields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Modals
{
    public class ProductDetails : Entity
    {
        public long productId { get; set; }
        public long SizeId { get; set; }
        public long TotalQuantity { get; set; }
        public double Price { get; set; }
        public double DiscountPrice { get; set; }

        public Product Product { get; set; }
        public Size Size { get; set; }

    }
}
