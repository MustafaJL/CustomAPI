using MediatR;
using Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.CommandHandler
{
    public class AddBrandCommandHandler : IRequestHandler<AddBrandCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddBrandCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(AddBrandCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.Brand.Add(new Domain.Modals.Brand
                {
                    BrandName = request.brandDTO.brandName
                });

                _unitOfWork.Save();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }
    }
}
