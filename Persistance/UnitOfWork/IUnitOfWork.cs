using Persistance.Repository;
using Persistance.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }

        IRoleRepository Roles { get; }

        IProductRepository Products { get; }

        IProductDetailsRepository ProductDetails { get; }

        ISizeRepository Size { get; }

        IBrandRepository Brand { get; }
        ICategoryRepository Category { get; }
        int Save(); 
    }
}
