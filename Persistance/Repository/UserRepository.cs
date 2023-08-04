using Domain.Modals;
using Infrastructure.Data;
using Persistance.Repository.Base;
using Persistance.Repository.IBase;
using Persistance.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
