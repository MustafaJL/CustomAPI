using Domain.Modals;
using MediatR;
using Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.CommandHandler
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            Category category = await _unitOfWork.Category.GetById(request.categoryDTO.Id);
            if (category != null)
            {
                try
                {
                    category.CategoryName = request.categoryDTO.categoryName;
                    _unitOfWork.Category.Update(category);
                    _unitOfWork.Save();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }

            }
            return false;

        }
    }
}
