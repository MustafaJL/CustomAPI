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
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace CustomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]

    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetProducts/{brandIds}")]
        public async Task<List<ProductViewModel>> GetProducts(string brandIds)
        {
            try
            {
                var query = new GetProductsQuery(brandIds);
                var response = await _mediator.Send(query);
                return await Task.FromResult(response);
            }
            catch
            {
                return await Task.FromResult(new List<ProductViewModel>());
            }

        }

        [HttpGet]
        [Route("GetProductById/{productId}")]
        public async Task<GetProductByIdDTO> GetProductById(long productId)
        {
            try
            {
                var query = new GetProductByIdQuery(productId);
                var response = await _mediator.Send(query);
                return response;
            }
            catch(Exception ex)
            {
                return new GetProductByIdDTO();
            }
        }

        [HttpPost]
        [Route("AddProduct")]
        public async Task<bool> AddProduct([FromForm] AddProductDTO productDTO)
        {
            try
            {
                var addProductCommand = new AddProductCommand(productDTO);
                var addProductCommandResponse = await _mediator.Send(addProductCommand);

                List<ProductConfigurationDTO> productConfigurationDTOs = JsonConvert.DeserializeObject<List<ProductConfigurationDTO>>(productDTO.configurations);
                var addProductDetialsCommand = new AddProductDetailsCommand(productConfigurationDTOs, addProductCommandResponse);
                var addProductDetialsCommandResponse = await _mediator.Send(addProductDetialsCommand);

                return true;
                
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpDelete]
        [Route("deleteProductById/{porudctId}")]
        public async Task<bool> DeleteProductById(long porudctId)
        {
            try
            {
                var command = new DeleteProductCommand(porudctId);
                var response = await _mediator.Send(command);
                return response;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        [HttpPut]
        [Route("updateProduct/{productId}")]
        public async Task<bool> UpdateProduct(long productId, [FromForm] AddProductDTO productDTO)
        {
            try
            {
                var command = new UpdateProductCommand(productId, productDTO);
                var response = await _mediator.Send(command);
                return response;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        [HttpGet]
        [Route("GetProductDetailsById/{productId}")]
        public async Task<IActionResult> GetProductDetailsById(long productId)
        {
            return Ok();
        }
    }
}
