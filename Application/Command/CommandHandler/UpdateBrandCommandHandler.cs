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
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBrandCommandHandler(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            Brand brand  = await _unitOfWork.Brand.GetById(request.brandDTO.Id);
            if(brand != null)
            {
                try
                {
                    brand.BrandName = request.brandDTO.brandName;
                    _unitOfWork.Brand.Update(brand);
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
