using CustomAPI.ViewModel;
using Domain.Modals;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Persistance.DTO;
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

        public UserController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public JsonResult Register(UserDto request)
        {
            string defaultPassword = _configuration.GetSection("AppSettings:DefaultPassword").Value;

            CreatePasswordHash(defaultPassword, out byte[] passwordHash, out byte[] passwordSalt);
            User user = new User()
            {
                Id = 0,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = Convert.ToBase64String(passwordHash),
                Salt = Convert.ToBase64String(passwordSalt),
                RoleId = request.RoleId,
            };

            _unitOfWork.Users.Add(user);
            _unitOfWork.Save();


            return new JsonResult("Registered Successfully!");
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

                token = CreateToken(user);
            }



            UserInfoDto userInfo = new UserInfoDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Token = token,
                RoleId = user.RoleId,
            };
           
            return Ok(userInfo);
        }


        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers()
        {
            IEnumerable<User> users = await _unitOfWork.Users.GetAllUsers();
            List<UserViewModel> usersViewModel = new List<UserViewModel>();
            foreach(var user in users)
            {
                usersViewModel.Add(new UserViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Role = user.Role,
                });
            }

            return Ok(usersViewModel);

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

        private string CreateToken(User user)
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
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }


    }
}
