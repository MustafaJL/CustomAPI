using Domain.Constants;
using Domain.Modals;
using MediatR;
using Persistance.Services.IServices;
using Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.CommandHandler
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        #region Properties
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        #endregion
        #region Ctor
        public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }
        #endregion
        #region Public Actions
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            if (request.productId > 0)
            {
                try
                {
                    if (isConfigurationsDeleted(request.productId).Result)
                    {
                        if (DeleteProduct(request.productId).Result)
                        {
                            return true;
                        }                      
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;

        }
        #endregion

        #region Private Actions
        private async Task<bool> DeleteProductConfigurations(long productId)
        {
            try
            {
                List<ProductDetails> productDetails = await _unitOfWork.ProductDetails.GetProductDetailsByProductId(productId);
                _unitOfWork.ProductDetails.RemoveRange(productDetails);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private async Task<bool> isConfigurationsDeleted(long productId)
        {
            return await DeleteProductConfigurations(productId);
        }
        private async Task<bool> DeleteProduct(long productId)
        {
            try
            {
                var product = await _unitOfWork.Products.GetById(productId);
                _fileService.DeleteImage(product.ImagePath, AppConstans.PRODUCTS_IMAGE);
                _unitOfWork.Products.Delete(product);
                _unitOfWork.Save();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        #endregion
    }
}
