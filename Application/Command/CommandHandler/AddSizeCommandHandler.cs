using MediatR;
using Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.CommandHandler
{
    public class AddSizeCommandHandler : IRequestHandler<AddSizeCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddSizeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(AddSizeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.Size.Add(new Domain.Modals.Size
                {
                    size = request.sizeDTO.size
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
