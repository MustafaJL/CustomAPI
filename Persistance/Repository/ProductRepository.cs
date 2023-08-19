using CustomAPI.ViewModel;
using Domain.Constants;
using Domain.Modals;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Persistance.Repository.Base;
using Persistance.Repository.IBase;
using Persistance.Repository.IRepository;
using Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ProductViewModel>> GetProducts(string language)
        {
            List<ProductViewModel> productViewModels = new List<ProductViewModel>();
            
            switch(language)
            {
                case Language.ENGLISH:
                    productViewModels = await _context
                                    .Products
                                    .Include(x => x.Category)
                                    .Include(x => x.Brand)
                                    .Include(x => x.ProductDetails)
                                    .ThenInclude(x => x.Size)
                                    .Select(x => new ProductViewModel
                                    {
                                        Id = x.Id,
                                        Name = x.ProductNameEng,
                                        Category = x.Category.CategoryNameEng,
                                        Brand = x.Brand.BrandNameEng,
                                        Description = x.DescriptionEng,
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
                    break;
                case Language.BRAZIL:
                    productViewModels = await _context
                                    .Products
                                    .Include(x => x.Category)
                                    .Include(x => x.Brand)
                                    .Include(x => x.ProductDetails)
                                    .ThenInclude(x => x.Size)
                                    .Select(x => new ProductViewModel
                                    {
                                        Id = x.Id,
                                        Name = x.ProductNameBraz,
                                        Category = x.Category.CategoryNameBraz,
                                        Brand = x.Brand.BrandNameBraz,
                                        Description = x.DescriptionBraz,
                                        imagePath = getImageByPath(x.ImagePath),
                                        productDetails = x.ProductDetails                                                               
                                                                .Select(x => new ProductDetailsViewModel
                                                                {
                                                                    sizeId = x.SizeId,
                                                                    TotalQuantity = x.TotalQuantity,
                                                                    sizeName = x.Size.size,
                                                                    Price = x.Price,
                                                                })
                                                                .ToList()
                                    })
                                    .ToListAsync();
                    break;
                default:
                    break;
            }

            return productViewModels;


        }

        private static string getImageByPath(string path)
        {

            var imagePath = Path.Combine("C:", "PMS" ,"ProductsImage", path);
            string extension = Path.GetExtension(path)?.TrimStart('.');
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
