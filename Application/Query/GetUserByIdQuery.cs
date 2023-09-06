using Domain.Modals;
using MediatR;
using Persistance.DTO;


namespace Application.Query
{

    public record GetUserByIdQuery(UserDto UserDto) : IRequest<User>;
}
