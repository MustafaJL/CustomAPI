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
        [Route("getCategories/{language}")]
        public async Task<IActionResult> getCategories(string language)
        {
            var query = new CategoryQuery(language);
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
