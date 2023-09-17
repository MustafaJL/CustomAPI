using Domain.Modals;
using MediatR;
using Persistance.DTO;
using Persistance.DTO.Shared;

namespace Application.Query
{

    public record GetUserByIdQuery(long userId) : IRequest<AddUserDTO>;
}
