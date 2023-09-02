using Domain.Modals;
using MediatR;
using Persistance.DTO;


namespace Application.Query
{

    public record GetCategoryByIdQuery(CategoryDTO CategoryDTO) : IRequest<Category>;
}
