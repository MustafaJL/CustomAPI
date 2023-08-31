
using MediatR;
using Persistance.DTO.Shared;
using Persistance.UnitOfWork;

namespace Application.Query.QueryHandler
{
    public class RoleQueryHandler : IRequestHandler<RoleQuery, List<LabelValueDto>>
    {

        private readonly IUnitOfWork _unitOfWork;

        public RoleQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<LabelValueDto>> Handle(RoleQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Roles.getRoles();
        }
    }
}
