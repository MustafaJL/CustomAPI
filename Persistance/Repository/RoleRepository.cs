using Domain.Modals;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Persistance.DTO.Shared;
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
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<LabelValueDto>> getRoles()
        {
            List<LabelValueDto> roles = new List<LabelValueDto>();


            roles = await _context
                                    .Roles
                                    .Select(x => new LabelValueDto
                                    {
                                        label = x.RoleName,
                                        value = x.Id.ToString(),
                                    })
                                    .ToListAsync();


            return roles;
        }
    }

}