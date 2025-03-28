using Application.DTO;
using Application.Features.CQRS.Queries;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CQRS.Handlers
{
    public class UserQueryHandler : IRequestHandler<UserQueryRequest, UserResponseDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        public UserQueryHandler(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<UserResponseDTO> Handle(UserQueryRequest request, CancellationToken cancellationToken)
        {
            var dto = new UserResponseDTO();
            var user = await _userRepository.GetByFilterAsync(x => x.Name == request.UserName && x.Password == request.Password);
            if (user == null)
            {
                dto.IsExist = false;
            }
            else
            {
                dto.Username = user.Name;
                dto.Id = user.Id;
                dto.IsExist = true;
                var role = await _roleRepository.GetByFilterAsync(x => x.Id == user.RoleId);
                dto.Role = role?.Definition;
            }
            return dto;
        }
    }
}
