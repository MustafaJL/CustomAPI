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

    //public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>

    //{
    //    private readonly IUnitOfWork _unitOfWork;
    //    private readonly IUserRepository _userRepository;

    //    public GetUserByIdQueryHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
    //    {
    //        _unitOfWork = unitOfWork;
    //       _userRepository = userRepository;
    //    }

    //    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    //    {
    //        // Fetch the user by ID from your repository
    //        User user = await _userRepository.GetUserById(request.UserDto.Id);

    //        // Map the User entity to a UserDto if needed
    //        UserDto userDto = MapToUserDto(user);

    //        return userDto;
    //    }

    //    private UserDto MapToUserDto(User user)
    //    {
    //        // Implement your mapping logic here if needed
    //        // You can use AutoMapper or manually map properties
    //        // For example:
    //        return new UserDto
    //        {
    //            Id = user.Id,
    //            FirstName = user.FirstName,
    //            LastName = user.LastName,
    //            Email = user.Email,
    //            Address = user.Address,
    //            Gender = user.Gender,
    //            DateOfBirth = user.DateOfBirth,
    //            PhoneNumber = user.PhoneNumber,
    //            RoleId = user.RoleId,


    //        };
    //    }
    //}
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>

    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {

            User user = await _unitOfWork.Users.GetById(request.UserDto.Id);
            return user;
        }
    }
}

