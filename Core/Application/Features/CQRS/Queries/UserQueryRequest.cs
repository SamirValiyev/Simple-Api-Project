using Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CQRS.Queries
{
    public class UserQueryRequest:IRequest<UserResponseDTO>
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
