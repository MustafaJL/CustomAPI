﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.DTO
{
    public class AddProductDTO
    {
      
            public long brandId { get; set; }
            public long categoryId { get; set; }
            public string configurations { get; set; }
            public string productDescription { get; set; }
            public string productName { get; set; }
            public IFormFile? productImage { get; set; }
        
    }
}
