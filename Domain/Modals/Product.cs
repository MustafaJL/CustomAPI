using Domain.Modals.SharedFields;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Modals
{
    public class Product :Entity
    {
        [Required]
        public string ProductNameEng { get; set; } = string.Empty;
        public string ProductNameBraz { get; set; } = string.Empty;

        public string DescriptionEng { get; set; } = string.Empty;
        public string DescriptionBraz { get; set; } = string.Empty;
        [Required]
        public string AdditionalData { get; set; } = string.Empty;
        [Required]
        public long CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        [Required]
        public long BrandId { get; set; }
        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public ICollection<ProductDetails> ProductDetails { get; set;}
        public ICollection<Size> Size { get; set; }

    }
}
