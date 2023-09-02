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

    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, Role>

    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRoleByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Role> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {

            Role role = await _unitOfWork.Roles.GetById(request.RoleDTO.Id);
            return role;
        }
    }

}
