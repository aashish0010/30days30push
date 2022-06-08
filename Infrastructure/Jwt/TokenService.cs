using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Jwt
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;


        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration configuration)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:tokenkey"]));

        }
        public string TokenGenerateString(string username)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId,username)
            };
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds

            };
            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.CreateToken(tokenDescripter);
            return tokenhandler.WriteToken(token);
        }
    }
}
