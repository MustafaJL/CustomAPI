using CustomAPI.ViewModel;
using Domain.Modals;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistance.DTO;
using Persistance.UnitOfWork;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Application.Command;
using Application.Query;

namespace CustomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetProducts")]
        public async Task<List<ProductViewModel>> GetProducts()
        {
            try
            {
                var query = new GetProductsQuery();
                var response = await _mediator.Send(query);
                return await Task.FromResult(response);
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
                var addProductCommand = new AddProductCommand(productDTO);
                var addProductCommandResponse = await _mediator.Send(addProductCommand);

                var addProductDetialsCommand = new AddProductDetailsCommand(productDTO.configurations, addProductCommandResponse);
                var addProductDetialsCommandResponse = await _mediator.Send(addProductDetialsCommand);

                return Ok("Product Added successfully");
                
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
