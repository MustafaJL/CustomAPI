using Application.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

    }
}
