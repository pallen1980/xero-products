using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XeroProducts.BL.Interfaces;

namespace XeroProducts.BL.Security
{
    public class JwtTokenProvider : IJwtTokenProvider
    {
        private readonly Lazy<IConfiguration> _configuration;

        protected IConfiguration Configuration => _configuration.Value;

        public JwtTokenProvider(Lazy<IConfiguration> configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Generate a Jwt token for the given username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public virtual string GenerateJwtToken(string userName)
        {
            var keyBytes = JwtTokenProvider.GetJwtKeyBytes(Configuration);

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
                Issuer = Configuration.GetValue<string>("XeroProducts:Auth:JwtConfig:ValidIssuer"),

                Expires = DateTime.UtcNow.AddMinutes(30),
                
                Audience = Configuration.GetValue<string>("XeroProducts:Auth:JwtConfig:ValidAudience"),
                
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
            var key = config.GetValue<string>("XeroProducts:Auth:JwtConfig:Key");

            if (string.IsNullOrEmpty(key))
            {
                throw new Exception("Missing Configuration: XeroProducts__Auth__JwtConfig__Key");
            }

            return Encoding.ASCII.GetBytes(key);
        }
    }
}
