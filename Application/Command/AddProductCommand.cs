﻿using MediatR;
using Persistance.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command
{
    public record AddProductCommand(AddProductDTO productDTO) : IRequest<long>;
    
}
