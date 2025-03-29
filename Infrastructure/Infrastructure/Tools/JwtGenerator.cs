using Application.DTO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tools
{
    public class JwtGenerator
    {
        public static TokenResponseDTO TokenGenerate(UserResponseDTO dto)
        {
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken();
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

        }
    }
}
