﻿using CustomAPI.ViewModel;
using MediatR;
using Persistance.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Query
{
    public record GetProductByIdQuery(long productId) : IRequest<GetProductByIdDTO>;
    
    
}
