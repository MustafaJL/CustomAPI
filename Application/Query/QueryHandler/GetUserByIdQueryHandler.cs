using Domain.Modals;
using MediatR;
using Persistance.DTO;
using Persistance.DTO.Shared;
using Persistance.Repository.IRepository;
using Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Query.QueryHandler
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, AddUserDTO>

    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AddUserDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {

            AddUserDTO user = await _unitOfWork.Users.GetUserById(request.userId);
            return user;
        }
    }
}

