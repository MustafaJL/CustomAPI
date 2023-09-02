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

    public class GetSizeByIdQueryHandler : IRequestHandler<GetSizeByIdQuery, Size>

    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSizeByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Size> Handle(GetSizeByIdQuery request, CancellationToken cancellationToken)
        {

            Size size = await _unitOfWork.Size.GetById(request.SizeDTO.Id);
            return size;
        }
    }

}
