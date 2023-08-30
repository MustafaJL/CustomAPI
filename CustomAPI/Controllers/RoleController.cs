using Application.Command;
using Application.Query;
using Domain.Modals;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistance.DTO;
using Persistance.UnitOfWork;

namespace CustomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase

    {
        private readonly IMediator _mediator;
    

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
           
        }


        [HttpGet]
        [Route("getRoles")]
       
        public async Task<IActionResult> getRoles()
        {

            //var roles = await _sender.Send(new RoleQuery());

            var roles = new RoleQuery();
            var response = await _mediator.Send(roles);
            return Ok(response);
        }


        [HttpPost]
        [Route("addRole")]
        public async Task<IActionResult> addRole(RoleDTO roleDTO)
        {
            var command = new AddRoleCommand(roleDTO);
            var response = await _mediator.Send(command);
            if (response)
            {
                return Ok("Role has been added successfuly!");
            }
            return BadRequest("Error Occured");
        }





        [HttpPut]
        [Route("updateRole")]
        public async Task<IActionResult> updateRole(RoleDTO roleDTO)
        {
            var command = new UpdateRoleCommand(roleDTO);
            var response = await _mediator.Send(command);
            if (response)
            {
                return Ok("role has been updated successfully");
            }
            return BadRequest("Error Occured");
        }
        [HttpDelete]
        [Route("deleteRoleById/{roleId}")]
        public async Task<IActionResult> deleteRoleById(long roleId)
        {
            var command = new DeleteRoleCommand(roleId);
            var response = await _mediator.Send(command);
            if (response)
            {
                return Ok($"Role with Id {roleId} has been deleted successfully");
            }
            return BadRequest("Error Occured");
        }

        //[HttpGet]
        //public async Task<IActionResult> GetRoles()
        //{
        //    IEnumerable<Role> rolesList = await _mediator.Roles.GetAll();

        //    return Ok(rolesList);
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddRole(Role role)
        //{
        //    await _mediator.Roles.Add(role);
        //    _mediator.Save();
        //    return Ok(role);
        //}


    }
}
