using Domain.Modals;
using MediatR;
using Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.CommandHandler
{

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User user = await _unitOfWork.Users.GetById(request.userDto.Id);
            if (user != null)
            {
                try
                {
                    user.Address = request.userDto.Address;
                    user.RoleId = request.userDto.roleId;
                    user.FirstName = request.userDto.firstName;
                    user.LastName = request.userDto.lastName;
                    user.Email = request.userDto.Email;
                    user.Gender = request.userDto.Gender;
                    user.DateOfBirth = request.userDto.dateOfBirth;
                    user.PhoneNumber = request.userDto.phoneNumber;
                    user.isActive = request.userDto.isActive;

                    _unitOfWork.Users.Update(user);
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
