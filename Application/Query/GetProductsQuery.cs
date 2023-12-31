﻿using CustomAPI.ViewModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Query
{
    public record GetProductsQuery(string brandIds) : IRequest<List<ProductViewModel>>;
    
}
