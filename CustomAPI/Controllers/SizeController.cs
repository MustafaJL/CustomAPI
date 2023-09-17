using Application.Command;
using Application.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistance.DTO;
using Persistance.DTO.Shared;

namespace CustomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class SizeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SizeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("getSizes")]
        
        public async Task<IActionResult> getSizes()
        {
            var list = new SizeQuery();
            var response = await _mediator.Send(list);
            return Ok(response);
        }

        [HttpGet]
        [Route("getSizeById/{sizeId}")]
        public async Task<IActionResult> getSizeById(long sizeId)
        {
            var query = new GetSizeByIdQuery(new SizeDTO { Id = sizeId });
            var response = await _mediator.Send(query);

            if (response != null)
            {
                return Ok(response);
            }
            return NotFound($"Size with Id {sizeId} not found");
        }

        [HttpPost]
        [Route("addSize")]
        public async Task<bool> addSize(SizeDTO sizeDTO)
        {
            try
            {
                var command = new AddSizeCommand(sizeDTO);
                var response = await _mediator.Send(command);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }




        [HttpPut]
        [Route("updateSize")]
        public async Task<bool> updateSize(SizeDTO sizeDTO)
        {
            try
            {
                var command = new UpdateSizeCommand(sizeDTO);
                var response = await _mediator.Send(command);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
            
        }
        [HttpDelete]
        [Route("deleteSizeById/{sizeId}")]
        public async Task<bool> deleteSizeById(long sizeId)
        {
            try
            {
                var command = new DeleteSizeCommand(sizeId);
                var response = await _mediator.Send(command);
                return response;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
