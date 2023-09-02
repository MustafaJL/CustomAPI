using Domain.Modals;
using MediatR;
using Persistance.DTO;
using Persistance.DTO.Shared;
using Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Query.QueryHandler
{

    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Category>

    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {

            Category category = await _unitOfWork.Category.GetById(request.CategoryDTO.Id);
            return category;
        }
    }

}
