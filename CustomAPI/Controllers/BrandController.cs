using Application.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [Route("getBrands/{language}")]
        public async Task<IActionResult> getBrands(string language)
        {
            var a = new BrandQuery(language);
            var response = await _mediator.Send(a);
            return Ok(response);
        }
    }
}
