﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CQRS.Commands.Create
{
    public class RegisterUserCommandRequest:IRequest
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
