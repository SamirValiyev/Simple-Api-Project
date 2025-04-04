using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CQRS.Commands.Create
{
    public class CreateCategroyCommandRequest:IRequest
    {
        public string? Definition { get; set; }

        public string Name { get; set; }
    }
}
