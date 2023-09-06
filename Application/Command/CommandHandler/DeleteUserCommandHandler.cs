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
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            User user = await _unitOfWork.Users.GetById(request.userId);
            if (user != null)
            {
                try
                {
                    _unitOfWork.Users.Delete(user);
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
