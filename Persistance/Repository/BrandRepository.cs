using CustomAPI.ViewModel;
using Domain.Constants;
using Domain.Modals;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Persistance.DTO.Shared;
using Persistance.Repository.Base;
using Persistance.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repository
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        private readonly ApplicationDbContext _context;

        public BrandRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<LabelValueDto>> getBrands()
        {
            List<LabelValueDto> brands = new List<LabelValueDto>();
           
        
            brands = await _context
                                    .Brands
                                    .Select(x => new LabelValueDto
                                    {
                                        label = x.BrandName,
                                        value = x.Id.ToString(),
                                    })
                                    .ToListAsync();
                
            
            return brands;
        }
    }
}
