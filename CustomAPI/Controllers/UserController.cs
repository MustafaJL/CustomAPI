using Domain.Modals;
using Microsoft.AspNetCore.Mvc;
using Persistance.UnitOfWork;

namespace CustomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            IEnumerable<User> users = await _unitOfWork.Users.GetAll();

            return Ok(users);

        }


    }
}
