using Domain.Modals;
using Persistance.DTO.Shared;
using Persistance.Repository.IBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<List<LabelValueDto>> getCategories();
    }
}
