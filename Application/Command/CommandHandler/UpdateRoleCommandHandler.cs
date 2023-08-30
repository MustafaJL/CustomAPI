using Domain.Modals;
using MediatR;
using Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.CommandHandler
{

    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRoleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            Role role = await _unitOfWork.Roles.GetById(request.roleDTO.Id);
            if (role != null)
            {
                try
                {
                    role.RoleName = request.roleDTO.roleName;
                    _unitOfWork.Roles.Update(role);
                    _unitOfWork.Save();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }

            }
            return false;

        }
    }
}
