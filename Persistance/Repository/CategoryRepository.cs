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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<LabelValueDto>> getCategories(string language)
        {
            List<LabelValueDto> categories = new List<LabelValueDto>();

            switch (language)
            {
                case Language.ENGLISH:
                    categories = await _context
                                            .Categories
                                            .Select(x => new LabelValueDto
                                            {
                                                label = x.CategoryNameEng,
                                                value = x.Id.ToString(),
                                            })
                                            .ToListAsync();
                    break;
                case Language.BRAZIL:
                    categories = await _context
                                            .Categories
                                            .Select(x => new LabelValueDto
                                            {
                                                label = x.CategoryNameBraz,
                                                value = x.Id.ToString(),
                                            })
                                            .ToListAsync();
                    break;
                default:
                    break;
            }
            return categories;
        }
    }
}
