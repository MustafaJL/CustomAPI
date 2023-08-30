using MediatR;
using Persistance.DTO.Shared;

namespace Application.Query
{
    
    public record RoleQuery() :IRequest<List<LabelValueDto>>;

}
