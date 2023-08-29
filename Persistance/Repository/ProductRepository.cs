using CustomAPI.ViewModel;
using Domain.Constants;
using Domain.Modals;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistance.Repository.Base;
using Persistance.Repository.IBase;
using Persistance.Repository.IRepository;
using Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public ProductRepository(ApplicationDbContext context, IConfiguration configuration) : base(context)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<ProductViewModel>> GetProducts()
        {
            List<ProductViewModel> productViewModels = new List<ProductViewModel>();
            
           
                    productViewModels = await _context
                                    .Products
                                    .Include(x => x.Category)
                                    .Include(x => x.Brand)
                                    .Include(x => x.ProductDetails)
                                    .ThenInclude(x => x.Size)
                                    .Select(x => new ProductViewModel
                                    {
                                        Id = x.Id,
                                        Name = x.ProductName,
                                        Category = x.Category.CategoryName,
                                        Brand = x.Brand.BrandName,
                                        Description = x.Description,
                                        imagePath = getImageByPath(x.ImagePath),
                                        productDetails =  x.ProductDetails
                                                                .Select(x => new ProductDetailsViewModel
                                                                {
                                                                    sizeId= x.SizeId,
                                                                    TotalQuantity= x.TotalQuantity,
                                                                    sizeName = x.Size.size,
                                                                    Price= x.Price,                                                                   
                                                                })
                                                                .ToList()
                                    })
                                    .ToListAsync();
                   
            

            return productViewModels;


        }

        private static string getImageByPath(string path)
        {

            var imagePath = Path.Combine("C:", "PMS" ,"ProductsImage", path);
            string? extension = Path.GetExtension(path)?.TrimStart('.');
            if (System.IO.File.Exists(imagePath))
            {
                var imageBytes = System.IO.File.ReadAllBytes(imagePath);
                var base64String = Convert.ToBase64String(imageBytes);
                return $"data:image/{extension};base64," + base64String;
            }
            else
            {
                return "";
            }
        }

    }
}
