using Application.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("getCategories")]
        public async Task<IActionResult> getCategories()
        {
            var query = new CategoryQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
