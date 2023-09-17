using Application.Command;
using Application.Query;
using CustomAPI.ViewModel;
using Domain.Modals;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Persistance.DTO;
using Persistance.DTO.Shared;
using Persistance.UnitOfWork;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CustomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;

        public UserController(IUnitOfWork unitOfWork, IConfiguration configuration, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mediator = mediator;
        }

        [HttpPost("AddUser")]
        public async Task<bool> AddUser(AddUserDTO request)
        {
            

            try
            {
                string defaultPassword = _configuration.GetSection("AppSettings:DefaultPassword").Value;

                CreatePasswordHash(defaultPassword, out byte[] passwordHash, out byte[] passwordSalt);

                var addUser = new AddUserCommand(request, passwordSalt , passwordHash);
                var addUserResponse = await _mediator.Send(addUser);

                return addUserResponse;

            }
            catch (Exception ex)
            {
                return false;
            }




        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto request)
        {
            var user = await _unitOfWork.Users.GetUserByEmail(request.Email);
            string token = "";
            if (user == null)
            {
                return BadRequest(new JsonResult("User not found!"));
            }
            else
            {

                if (!VerifyPasswordHash(request.Password, Convert.FromBase64String(user.Password), Convert.FromBase64String(user.Salt)))
                {
                    return BadRequest(new JsonResult("Invalid Password"));
                }

                token = CreateToken(user , request.RememberMe);
            }



            LoginResponse userInfo = new LoginResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Token = token,
                RoleName = user.Role.RoleName,
            };

            return Ok(userInfo);
        }




        [HttpGet]
        [Route("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var list = new GetAllUsersQuery();
            var response = await _mediator.Send(list);
            return Ok(response);

        }


        [HttpGet]
        [Route("GetUserById/{userId}")]
        public async Task<IActionResult> GetUserById(long userId)
        {

            var query = new GetUserByIdQuery(userId);
            var response = await _mediator.Send(query);

            if (response == null)
            {
                return NotFound($"User with Id {userId} not found");
               
            }
            return Ok(response);

        }

        [HttpPut]
        [Route("UpdateUser")]
        public async Task<bool> UpdateUser(AddUserDTO userDto)
        {
            var command = new UpdateUserCommand(userDto);
            var response = await _mediator.Send(command);
            if (response)
            {
                return true;
            }
            return false;
        }

        [HttpDelete]
        [Route("DeleteUserById/{userId}")]
        public async Task<bool> DeleteUserById(long userId)
        {
            var command = new DeleteUserCommand(userId);
            var response = await _mediator.Send(command);
            if (response)
            {
                return true;
            }
            return false;
        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                // hmac automatically create a salt
                passwordSalt = hmac.Key;
                // create passowrd hash
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            var hmac = new HMACSHA512(passwordSalt);
            var newPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return newPassword.SequenceEqual(passwordHash);
        }

        private string CreateToken(User user , bool rememberMe)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FirstName + user.LastName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role,  _unitOfWork.Roles.GetById(user.RoleId).Result.RoleName),
                new Claim("userId", user.Id.ToString()),

            };
            
            var key = new SymmetricSecurityKey(Encoding
                .UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value)
                );

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: rememberMe ? DateTime.Now.AddDays(30) : DateTime.Now.AddMinutes(60),
                signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        
    }
}
