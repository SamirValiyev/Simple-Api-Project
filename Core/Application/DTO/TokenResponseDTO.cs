using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class TokenResponseDTO
    {
        public TokenResponseDTO(string token,DateTime expireDate)
        {
            AccessToken = token;
            ExpireDate = expireDate;
        }
        public string AccessToken { get; set; }
       // public string RefreshToken { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
