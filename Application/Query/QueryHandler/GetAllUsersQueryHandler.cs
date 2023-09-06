
using MediatR;
using Persistance.DTO;
using Persistance.DTO.Shared;
using Persistance.UnitOfWork;

namespace Application.Query.QueryHandler
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {

        private readonly IUnitOfWork _unitOfWork;

        public GetAllUsersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return (List<UserDto>)await _unitOfWork.Users.GetAllUser();
        }
       
    }
}
