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
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRoleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            Role role = await _unitOfWork.Roles.GetById(request.roleId);
                  if (role != null)
            {
                try
                {
                    _unitOfWork.Roles.Delete(role);
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
