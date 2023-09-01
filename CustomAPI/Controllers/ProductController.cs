using CustomAPI.ViewModel;
using Domain.Modals;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistance.DTO;
using Persistance.UnitOfWork;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("GetProducts")]
        public async Task<List<ProductViewModel>> GetProducts()
        {
            try
            {
                return await Task.FromResult(await _unitOfWork.Products.GetProducts());
            }
            catch
            {
                return await Task.FromResult(new List<ProductViewModel>());
            }

        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(AddProductDTO productDTO)
        {
            try
            {
                Product product = new()
                {
                    ProductName = productDTO.productName,
                    Description = productDTO.productDescription,
                    CategoryId = productDTO.categoryId,
                    BrandId = productDTO.brandId,
                    ImagePath = "test.jpg"
                };
                await _unitOfWork.Products.Add(product);
                _unitOfWork.Save();
                

                await _unitOfWork.ProductDetails.AddRangeAsync(productDTO.configurations.Select(x => new ProductDetails
                {
                    productId = product.Id,
                    SizeId = Convert.ToInt64(x.sizeId),
                    Price = x.price,
                    TotalQuantity = x.totalQuantity
                }).ToList());
                _unitOfWork.Save();


                  
                return Ok("Product Added successfully");
                
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
