using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using XeroProducts.BL.Interfaces;

namespace XeroProducts.BL.Security
{
    public class JwtTokenProvider : IJwtTokenProvider
    {
        private readonly IConfiguration _configuration;

        public JwtTokenProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Generate a Jwt token for the given username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GenerateJwtToken(string userName)
        {
            var keyBytes = JwtTokenProvider.GetJwtKeyBytes(_configuration);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, userName)
                    }
                ),
                
                IssuedAt = DateTime.UtcNow,
                Issuer = _configuration.GetValue<string>("Auth:JwtConfig:ValidIssuer"),

                Expires = DateTime.UtcNow.AddMinutes(30),
                
                Audience = _configuration.GetValue<string>("Auth:JwtConfig:ValidAudience"),
                
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Returns the Jwt key from configuration
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static byte[] GetJwtKeyBytes(IConfiguration config)
        {
            var key = config.GetValue<string>("Auth:JwtConfig:Key");

            return Encoding.ASCII.GetBytes(key);
        }
    }
}
