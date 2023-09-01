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

    public class UpdateSizeCommandHandler : IRequestHandler<UpdateSizeCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSizeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateSizeCommand request, CancellationToken cancellationToken)
        {
            Size size = await _unitOfWork.Size.GetById(request.sizeDTO.Id);
            if (size != null)
            {
                try
                {
                    size.size = request.sizeDTO.size;
                    _unitOfWork.Size.Update(size);
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
