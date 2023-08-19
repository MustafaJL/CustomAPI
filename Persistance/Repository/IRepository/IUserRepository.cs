﻿using Domain.Modals;
using Persistance.Repository.IBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> GetUserByEmail(string email);
        public Task<IEnumerable<User>> GetAllUsers();
    }
}
