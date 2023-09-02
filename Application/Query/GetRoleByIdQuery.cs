using Domain.Modals;
using MediatR;
using Persistance.DTO;


namespace Application.Query
{

    public record GetRoleByIdQuery(RoleDTO RoleDTO) : IRequest<Role>;
}
