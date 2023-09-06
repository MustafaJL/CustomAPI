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
                    RoleId=request.userDto.RoleId,

                      Id = request.userDto.Id,
                    FirstName = request.userDto.FirstName,
                    LastName = request.userDto.LastName,
                    Email = request.userDto.Email,
                    Address = request.userDto.Address,
                    Gender = request.userDto.Gender,
                    DateOfBirth = request.userDto.DateOfBirth,
                    PhoneNumber = request.userDto.PhoneNumber,

                    
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
