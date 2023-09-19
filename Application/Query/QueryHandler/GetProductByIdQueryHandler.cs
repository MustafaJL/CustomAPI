using CustomAPI.ViewModel;
using MediatR;
using Persistance.DTO;
using Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Query.QueryHandler
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery , GetProductByIdDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        #region Properties
        #endregion

        #region Ctor
        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        #endregion

        #region Public Actions
        public Task<GetProductByIdDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var product = _unitOfWork.Products.GetProductById(request.productId);
                return product;
            }   
            catch (Exception ex)
            {
                throw new Exception("Error Occured");
            }
        }
        #endregion

        #region Private Actions
        #endregion
    }
}
