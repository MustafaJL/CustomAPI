using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Persistance.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Persistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IUserRepository Users { get; }
        public IRoleRepository Roles { get; }


        public UnitOfWork(ApplicationDbContext dbContext,
                            IUserRepository usersRepository,
                            IRoleRepository rolesRepository)
        {
            _context = dbContext;
            Users = usersRepository;
            Roles = rolesRepository;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
