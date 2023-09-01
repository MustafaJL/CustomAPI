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
    public class DeleteSizeCommandHandler : IRequestHandler<DeleteSizeCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSizeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteSizeCommand request, CancellationToken cancellationToken)
        {
            Size size = await _unitOfWork.Size.GetById(request.sizeId);
            if (size != null)
            {
                try
                {
                    _unitOfWork.Size.Delete(size);
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
