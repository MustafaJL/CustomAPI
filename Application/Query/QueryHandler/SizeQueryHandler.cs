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
    public class SizeQueryHandler : IRequestHandler<SizeQuery, List<LabelValueDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SizeQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<LabelValueDto>> Handle(SizeQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Size.getSizes();

        }
    }
}
