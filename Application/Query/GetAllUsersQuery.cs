using MediatR;
using Persistance.DTO.Shared;
using Persistance.DTO;
using CustomAPI.ViewModel;
using Persistance.DTO;

namespace Application.Query
{

    public record GetAllUsersQuery() : IRequest<List<UserDto>>;

}
