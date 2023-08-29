using CustomAPI.ViewModel;
using Domain.Constants;
using Domain.Modals;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistance.Repository.Base;
using Persistance.Repository.IBase;
using Persistance.Repository.IRepository;
using Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repository
{
    public class ProductDerailsRepository : Repository<ProductDetails>, IProductDetailsRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductDerailsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        
    }
}
