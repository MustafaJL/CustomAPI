using Domain.Modals;
using MediatR;
using Persistance.DTO;
using Persistance.DTO.Shared;
using Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Query.QueryHandler
{
    
    public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, Brand>

    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBrandByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Brand> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
           
                 Brand brand = await _unitOfWork.Brand.GetById(request.BrandDTO.Id);
                 return brand;
        }
    }

}
