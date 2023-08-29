using MediatR;
using Persistance.DTO.Shared;
using Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Query.QueryHandler
{
    public class BrandQueryHandler : IRequestHandler<BrandQuery, List<LabelValueDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<LabelValueDto>> Handle(BrandQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Brand.getBrands();  
        }
    }
}
