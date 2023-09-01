using MediatR;
using Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.CommandHandler
{
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.Category.Add(new Domain.Modals.Category
                {
                    CategoryName = request.categoryDTO.categoryName
                });

                _unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
