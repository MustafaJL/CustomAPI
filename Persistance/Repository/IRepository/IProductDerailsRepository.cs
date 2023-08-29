using CustomAPI.ViewModel;
using Domain.Modals;
using Persistance.Repository.IBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repository.IRepository
{
    public interface IProductDetailsRepository : IRepository<ProductDetails>
    {
    }
}
