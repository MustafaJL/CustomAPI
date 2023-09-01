using CustomAPI.ViewModel;
using MediatR;
using Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Query.QueryHandler
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductViewModel>>
    {
        #region Properties
        private readonly IUnitOfWork _unitOfWork;
        #endregion
        #region Ctor
        public GetProductsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion 
        #region Public Actions
        public async Task<List<ProductViewModel>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await Task.FromResult(await _unitOfWork.Products.GetProducts());
            }
            catch
            {
                return await Task.FromResult(new List<ProductViewModel>());
            }
        }
        #endregion
        #region Private Actions
        #endregion
    }
}
