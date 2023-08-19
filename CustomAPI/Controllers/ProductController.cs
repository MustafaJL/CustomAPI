using CustomAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistance.UnitOfWork;

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
        [Route("GetProducts/{language}")]
        public async Task<List<ProductViewModel>> GetProducts(string language)
        {
            try
            {
                return await Task.FromResult(await _unitOfWork.Products.GetProducts(language));
            }
            catch
            {
                return await Task.FromResult(new List<ProductViewModel>());
            }

        }
    }
}
