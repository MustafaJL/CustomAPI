using Application.Command;
using Application.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistance.DTO;

namespace CustomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private IMediator _mediator;
        
        public BrandController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("getBrands")]
        public async Task<IActionResult> getBrands()
        {
            var a = new BrandQuery();
            var response = await _mediator.Send(a);
            return Ok(response);
        }

        [HttpPost]
        [Route("addBrand")]
        public async Task<IActionResult> addBrand(BrandDTO brandDTO)
        {
            var command = new AddBrandCommand(brandDTO);
            var response = await _mediator.Send(command);
            if (response)
            {
                return Ok("Brand has been added successfuly!");
            }
            return BadRequest("Error Occured");
        }

        [HttpPut]
        [Route("updateBrand")]
        public async Task<IActionResult> updateBrand(BrandDTO brandDTO)
        {
            var command = new UpdateBrandCommand(brandDTO);
            var response = await _mediator.Send(command);
            if(response)
            {
                return Ok("Brand has been updated successfully");
            }
            return BadRequest("Error Occured");
        }

        [HttpDelete]
        [Route("deleteBrandById/{brandId}")]
        public async Task<IActionResult> deleteBrandById(long brandId)
        {
            var command = new DeleteBrandCommand(brandId);
            var response = await _mediator.Send(command);
            if (response)
            {
                return Ok($"Brand with Id {brandId} has been deleted successfully");
            }
            return BadRequest("Error Occured");
        }
    }
}
