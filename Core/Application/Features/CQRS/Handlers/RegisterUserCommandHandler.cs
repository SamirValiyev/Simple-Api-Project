using Application.Enums;
using Application.Features.CQRS.Commands.Create;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CQRS.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest>
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
        {
            await _userRepository.AddAsync(new Domain.User
            {
                Name=request.UserName,
                Password = request.Password,
                RoleId=(int)Role.Member
            });
        }
    }
}
