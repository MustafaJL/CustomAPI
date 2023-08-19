﻿using MediatR;
using Persistance.DTO.Shared;
using Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Query.QueryHandler
{
    public class CategoryQueryHandler : IRequestHandler<CategoryQuery, List<LabelValueDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<LabelValueDto>> Handle(CategoryQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Category.getCategories(request.language);
        }
    }
}
