using Domain.Modals.SharedFields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Modals
{
    public class Size : Entity
    {
        public string size { get; set; } = string.Empty;
        public ICollection<ProductDetails> ProductDetails { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
