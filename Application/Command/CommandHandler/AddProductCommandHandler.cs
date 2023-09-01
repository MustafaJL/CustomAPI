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
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, long>
    {
        private readonly IUnitOfWork _unitOfWork;
        #region Properties
        #endregion

        #region Ctor
        public AddProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Public Actions
        public async Task<long> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Product product = new()
                {
                    ProductName = request.productDTO.productName,
                    Description = request.productDTO.productDescription,
                    CategoryId = request.productDTO.categoryId,
                    BrandId = request.productDTO.brandId,
                    ImagePath = "test.jpg"
                };
                await _unitOfWork.Products.Add(product);
                _unitOfWork.Save();
                return product.Id;
            }
            catch (Exception ex)
            {
                return -1;
            }

        }
        #endregion

        #region Private Actions
        #endregion
    }
}
