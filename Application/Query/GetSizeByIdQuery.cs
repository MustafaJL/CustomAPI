using Domain.Modals;
using MediatR;
using Persistance.DTO;


namespace Application.Query
{

    public record GetSizeByIdQuery(SizeDTO SizeDTO) : IRequest<Size>;
}
