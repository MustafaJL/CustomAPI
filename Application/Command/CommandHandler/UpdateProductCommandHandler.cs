using Domain.Constants;
using Domain.Modals;
using MediatR;
using Newtonsoft.Json;
using Persistance.DTO;
using Persistance.Services.IServices;
using Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Command.CommandHandler
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        #region Properties
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        #endregion

        #region Ctor
        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }
        #endregion

        #region Public actions
        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            if (request.productId > 0)
            {
                try
                {
                    if (isConfigurationsDeleted(request.productId).Result)
                    {
                        if (UpdateProduct(request.productId,request.productDTO).Result)
                        {
                            if (AddProductConfigurations(request.productId, request.productDTO.configurations).Result){
                                return true;
                            }
                            return false;
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
        private async Task<bool> UpdateProduct(long productId, AddProductDTO productDTO)
        {
            try
            {
                var product = await _unitOfWork.Products.GetById(productId);
                if(productDTO.productImage != null)
                {
                    _fileService.DeleteImage(product.ImagePath, AppConstans.PRODUCTS_IMAGE);
                    product.ImagePath = _fileService.UploadImage(productDTO.productImage, AppConstans.PRODUCTS_IMAGE).Result;
                }
                
                product.ProductName = productDTO.productName;
                product.Description = productDTO.productDescription;
                product.CategoryId = productDTO.categoryId;
                product.BrandId = productDTO.brandId;
                
                _unitOfWork.Products.Update(product);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private async Task<bool> AddProductConfigurations(long productId, string productConfigurations)
        {
            try
            {
                List<ProductConfigurationDTO> configurations = JsonConvert.DeserializeObject<List<ProductConfigurationDTO>>(productConfigurations);
                await _unitOfWork.ProductDetails.AddRangeAsync(configurations.Select(x => new ProductDetails
                {
                    productId = productId,
                    SizeId = Convert.ToInt64(x.sizeId),
                    Price = x.price,
                    DiscountPrice = x.discountPrice,
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
    }
}
