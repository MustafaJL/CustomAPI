
using Domain.Modals;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CustomAPI.ViewModel;
using Persistance.DTO;
using Persistance.Repository.Base;
using Persistance.Repository.IBase;
using Persistance.Repository.IRepository;
using Persistance.Services.IServices;
using Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance.DTO.Shared;

namespace Persistance.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileService _fileService;
        public UserRepository(ApplicationDbContext context, IFileService fileService) : base(context)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<AddUserDTO> GetUserById(long userId)
        {
            AddUserDTO user = await _context.Users
                .Where(u => u.Id == userId)
                .Include(x => x.Role)
                .Select(x => new AddUserDTO
                {
                    Id = x.Id,
                    firstName = x.FirstName,
                    lastName = x.LastName,
                    Email = x.Email,
                    Address = x.Address,
                    phoneNumber = x.PhoneNumber,
                    dateOfBirth = x.DateOfBirth,
                    roleId = x.RoleId,
                    Gender = x.Gender,
                    isActive = x.isActive
                })
                .FirstOrDefaultAsync()
                ;

            if (user == null)
            {
                return new AddUserDTO();
            }

            return user;
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            IEnumerable<User> users = _context.Users.Where(x => x.isActive).Include(x => x.Role);

            return users;
        }


        public async Task<IEnumerable<UserDto>> GetAllUser()
        {
            List<UserDto> users = new List<UserDto>();


            users = await _context
                            .Users
                            .Include(x => x.Role)
                            .Select(x => new UserDto
                            {
                                Id = x.Id,
                                Name = x.FirstName + " " + x.LastName,
                                Email = x.Email,
                                Address = x.Address,
                                PhoneNumber = x.PhoneNumber,
                                RoleId = x.RoleId,
                                RoleName = x.Role.RoleName,
                                isActive = x.isActive
                                
                            })
                            .ToListAsync();



            return users;


        }

        public async Task<User> GetUserByEmail(string email)
        {
            User user = await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                return new User();
            }
            return user;
        }

    }
}
