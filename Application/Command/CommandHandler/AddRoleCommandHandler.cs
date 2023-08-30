using MediatR;
using Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.CommandHandler
{
    public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddRoleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.Roles.Add(new Domain.Modals.Role
                {
                    RoleName = request.roleDTO.roleName
                });

                _unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
