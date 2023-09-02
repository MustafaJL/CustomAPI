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

        [HttpGet]
        [Route("getCategoryById/{categoryId}")]
        public async Task<IActionResult> getCategoryById(long categoryId)
        {
            var query = new GetCategoryByIdQuery(new CategoryDTO { Id = categoryId });
            var response = await _mediator.Send(query);

            if (response != null)
            {
                return Ok(response);
            }
            return NotFound($"Category with Id {categoryId} not found");
        }

        [HttpPost]
        [Route("addCategory")]
        public async Task<IActionResult> addCategory(CategoryDTO categoryDTO)
        {
            var command = new AddCategoryCommand(categoryDTO);
            var response = await _mediator.Send(command);
            if (response)
            {
                return Ok("Category has been added successfuly!");
            }
            return BadRequest("Error Occured");
        }

        [HttpPut]
        [Route("updateCategory")]
        public async Task<IActionResult> updateCategory(CategoryDTO categoryDTO)
        {
            var command = new UpdateCategoryCommand(categoryDTO);
            var response = await _mediator.Send(command);
            if (response)
            {
                return Ok("Category has been updated successfully");
            }
            return BadRequest("Error Occured");
        }

        [HttpDelete]
        [Route("deleteCategoryById/{categoryId}")]
        public async Task<IActionResult> deleteCategoryById(long categoryId)
        {
            var command = new DeleteCategoryCommand(categoryId);
            var response = await _mediator.Send(command);
            if (response)
            {
                return Ok($"Category with Id {categoryId} has been deleted successfully");
            }
            return BadRequest("Error Occured");
        }
    }
}
