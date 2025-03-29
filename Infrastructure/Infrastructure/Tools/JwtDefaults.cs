using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tools
{
    public static class JwtDefaults
    {
        public static string ValidAudience;
        public static string ValidIssuer;
        public static string SecretKey;
        public static int Expire;

        public static void Initialize(IConfiguration configuration)
        {
            ValidAudience = configuration["Token:Audience"];
            ValidIssuer = configuration["Token:Issuer"];
            SecretKey = configuration["Token:SecurityKey"];
            Expire = int.Parse(configuration["Token:Expiration"]);
        }
    }
}
