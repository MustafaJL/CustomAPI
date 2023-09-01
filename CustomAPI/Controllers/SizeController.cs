using Application.Command;
using Application.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistance.DTO;
using Persistance.DTO.Shared;

namespace CustomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpPost]
        [Route("addSize")]
        public async Task<IActionResult> addSize(SizeDTO sizeDTO)
        {
            var command = new AddSizeCommand(sizeDTO);
            var response = await _mediator.Send(command);
            if (response)
            {
                return Ok("Size has been added successfuly!");
            }
            return BadRequest("Error Occured");
        }
        [HttpPut]
        [Route("updateSize")]
        public async Task<IActionResult> updateSize(SizeDTO sizeDTO)
        {
            var command = new UpdateSizeCommand(sizeDTO);
            var response = await _mediator.Send(command);
            if (response)
            {
                return Ok("Size has been updated successfully");
            }
            return BadRequest("Error Occured");
        }
        [HttpDelete]
        [Route("deleteSizeById/{sizeId}")]
        public async Task<IActionResult> deleteSizeById(long sizeId)
        {
            var command = new DeleteSizeCommand(sizeId);
            var response = await _mediator.Send(command);
            if (response)
            {
                return Ok($"Size with Id {sizeId} has been deleted successfully");
            }
            return BadRequest("Error Occured");
        }

    }
}
