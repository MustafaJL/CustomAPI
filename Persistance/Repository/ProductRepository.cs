using CustomAPI.ViewModel;
using Domain.Constants;
using Domain.Modals;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistance.DTO;
using Persistance.Repository.Base;
using Persistance.Repository.IBase;
using Persistance.Repository.IRepository;
using Persistance.Services.IServices;
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
        private readonly IFileService _fileService;

        public ProductRepository(ApplicationDbContext context, IConfiguration configuration, IFileService fileService) : base(context)
        {
            _context = context;
            _configuration = configuration;
            _fileService = fileService;
        }

        public async Task<List<ProductViewModel>> GetProducts(string brandsIds)
        {
            List<ProductViewModel> productViewModels = new List<ProductViewModel>();
            List<long> brandIdList = brandsIds.Split(',').ToList().Select(x => Convert.ToInt64(x)).ToList();
           
                    productViewModels = await _context
                                    .Products
                                    .Where(x => brandIdList.Contains(0) ? 1==1 : brandIdList.Contains(x.BrandId))
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
                                        imagePath = _fileService.GetImage(x.ImagePath, AppConstans.PRODUCTS_IMAGE),
                                        productDetails =  x.ProductDetails
                                                                .Select(x => new ProductDetailsViewModel
                                                                {
                                                                    sizeId= x.SizeId,
                                                                    TotalQuantity= x.TotalQuantity,
                                                                    sizeName = x.Size.size,
                                                                    Price= x.Price,      
                                                                    DiscountPrice= x.DiscountPrice,
                                                                })
                                                                .ToList()
                                    })
                                    .ToListAsync();
                   
            

            return productViewModels;


        }

        public async Task<GetProductByIdDTO> GetProductById(long productId)
        {


            var productViewModel = await _context
                            .Products
                            .Where(x => x.Id == productId)
                            .Include(x => x.ProductDetails)
                            .ThenInclude(x => x.Size)
                            .Select(x => new GetProductByIdDTO
                            {
                                Id = x.Id,
                                Name = x.ProductName,
                                categoryId = x.CategoryId,
                                brandId = x.BrandId,
                                Description = x.Description,
                                imagePath = _fileService.GetImage(x.ImagePath, AppConstans.PRODUCTS_IMAGE),
                                productDetails = x.ProductDetails
                                                        .Select(x => new ProductDetailsViewModel
                                                        {
                                                            sizeId = x.SizeId,
                                                            TotalQuantity = x.TotalQuantity,
                                                            sizeName = x.Size.size,
                                                            Price = x.Price,
                                                            DiscountPrice = x.DiscountPrice
                                                        })
                                                        .ToList()
                            })
                            .FirstOrDefaultAsync();



            return productViewModel;


        }

    }
}
