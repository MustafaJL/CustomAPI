using CustomAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.DTO
{
    public class GetProductByIdDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long brandId { get; set; }
        public long categoryId { get; set; }
        public string imagePath { get; set; }
        public List<ProductDetailsViewModel> productDetails { get; set; }
    }
}
