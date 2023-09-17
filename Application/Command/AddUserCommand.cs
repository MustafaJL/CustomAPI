using MediatR;
using Persistance.DTO;
using Persistance.DTO.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command
{
    public record AddUserCommand(AddUserDTO userDto, byte[] passwordSalt, byte[] password) : IRequest<bool>;
}
