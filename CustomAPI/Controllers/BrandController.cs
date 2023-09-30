using Application.Command;
using Application.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistance.DTO;

namespace CustomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]

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

        [HttpGet]
        [Route("getBrandById/{brandId}")]
        public async Task<IActionResult> getBrandById(long brandId)
        {
            var query = new GetBrandByIdQuery(new BrandDTO { Id = brandId });
            var response = await _mediator.Send(query);

            if (response != null)
            {
                return Ok(response);
            }
            return NotFound($"Brand with Id {brandId} not found");
        }




        [HttpPost]  
        [Route("addBrand")]
        public async Task<bool> addBrand(BrandDTO brandDTO)
        {

            try
            {
                var command = new AddBrandCommand(brandDTO);
                var response = await _mediator.Send(command);
                return true;
            }
            catch (Exception ex) { 
                return false;
            }

           
        }

        [HttpPut]
        [Route("updateBrand")]
        public async Task<bool> updateBrand(BrandDTO brandDTO)
        {
            try
            {
                var command = new UpdateBrandCommand(brandDTO);
                var response = await _mediator.Send(command);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpDelete]
        [Route("deleteBrandById/{brandId}")]
        public async Task<bool> deleteBrandById(long brandId)
        {
            try
            {
                var command = new DeleteBrandCommand(brandId);
                var response = await _mediator.Send(command);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
