using Domain.Modals;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistance.UnitOfWork;

namespace CustomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleController(IUnitOfWork unitOfWork)
        {
                _unitOfWork= unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            IEnumerable<Role> rolesList = await _unitOfWork.Roles.GetAll();

            return Ok(rolesList);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(Role role)
        {
            await _unitOfWork.Roles.Add(role);
            _unitOfWork.Save();
            return Ok(role);
        }

        
    }
}
