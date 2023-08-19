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
    public class SizeRepository : Repository<Size>, ISizeRepository
    {
        private readonly ApplicationDbContext _context;

        public SizeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<LabelValueDto>> getSizes()
        {
            List<LabelValueDto> sizes = await _context
                                                .Sizes
                                                .Select(x => new LabelValueDto
                                                {
                                                    label = x.size,
                                                    value = x.Id.ToString(),
                                                })
                                                .ToListAsync();
            return sizes;
        }
    }
}
