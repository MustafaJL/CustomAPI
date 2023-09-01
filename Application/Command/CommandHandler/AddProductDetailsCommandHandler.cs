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
    public class AddProductDetailsCommandHandler : IRequestHandler<AddProductDetailsCommand, bool>
    {

        #region Properties
        private readonly IUnitOfWork _unitOfWork;
        #endregion
        #region Ctor
        public AddProductDetailsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion
        #region Public Actions
        public async Task<bool> Handle(AddProductDetailsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.ProductDetails.AddRangeAsync(request.configurations.Select(x => new ProductDetails
                {
                    productId = request.productId,
                    SizeId = Convert.ToInt64(x.sizeId),
                    Price = x.price,
                    TotalQuantity = x.totalQuantity
                }).ToList());
                _unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Private Actions
        #endregion
    }
}
