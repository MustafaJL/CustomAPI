using Domain.Modals;
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

        public IProductRepository Products { get; }
        public ISizeRepository Size { get; }
        public IBrandRepository Brand { get; }
        public ICategoryRepository Category { get; }

        public IProductDetailsRepository ProductDetails {get; }

        public UnitOfWork(ApplicationDbContext dbContext,
                            IUserRepository usersRepository,
                            IRoleRepository rolesRepository,
                            IProductRepository productRepository,
                            ISizeRepository sizeRepository,
                            IBrandRepository brandRepository,
                            ICategoryRepository category,
                            IProductDetailsRepository productDetails)
        {
            _context = dbContext;
            Users = usersRepository;
            Roles = rolesRepository;
            Products = productRepository;
            Size = sizeRepository;
            Brand = brandRepository;
            Category = category;
            ProductDetails = productDetails;
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
