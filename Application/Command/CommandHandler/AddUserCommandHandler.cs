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
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.Users.Add(new Domain.Modals.User
                {
                    Id = 0,
                    FirstName = request.userDto.firstName,
                    LastName = request.userDto.lastName,
                    Email = request.userDto.Email,
                    Password = Convert.ToBase64String(request.password),
                    Salt = Convert.ToBase64String(request.passwordSalt),
                    RoleId = request.userDto.roleId,
                    PhoneNumber = request.userDto.phoneNumber,
                    Address = request.userDto.Address,
                    DateOfBirth = request.userDto.dateOfBirth,
                    Gender = request.userDto.Gender,
                    isActive = request.userDto.isActive


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
