using Application.DTO;
using Azure.Core;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tools
{
    public  class JwtGenerator
    {
      

        public TokenResponseDTO TokenGenerate(UserResponseDTO dto)
        {
           
            var claims = new List<Claim>();
            if (!string.IsNullOrEmpty(dto.Role))
                claims.Add(new Claim(ClaimTypes.Role, dto.Role));

            claims.Add(new Claim(ClaimTypes.NameIdentifier, dto.Id.ToString()));
            if (!string.IsNullOrEmpty(dto.Username))
                claims.Add(new Claim(ClaimTypes.Name, dto.Username));
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtDefaults.SecretKey));

            SigningCredentials credentials=new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expireDate=DateTime.UtcNow.AddDays(JwtDefaults.Expire);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: JwtDefaults.ValidIssuer,
                audience: JwtDefaults.ValidAudience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expireDate,
                signingCredentials:credentials
                );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return new TokenResponseDTO(tokenHandler.WriteToken(jwtSecurityToken),expireDate);

        }
    }
         
}
