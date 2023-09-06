
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

        public async Task<User> GetUserById(long userId)
        {
            User user = await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                return new User();
            }

            return user;
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            IEnumerable<User> users = _context.Users.Include(x => x.Role);

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
                                FirstName = x.FirstName,
                                LastName = x.LastName,
                                Email = x.Email,
                                Address = x.Address,
                                Gender=x.Gender,
                                DateOfBirth = x.DateOfBirth,
                                PhoneNumber = x.PhoneNumber,
                                RoleId = x.RoleId,
                               
                                
                            
                            })
                            .ToListAsync();



            return users;


        }

        public async Task<User> GetUserByEmail(string email)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                return new User();
            }
            return user;
        }

    }
}
