using MediatR;
using Persistance.DTO.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Query
{
    public record SizeQuery : IRequest<List<LabelValueDto>>;
    
}
