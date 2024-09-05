using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using XeroProducts.BL.Authentication;

namespace XeroProducts.BL.Interfaces
{
    public interface IJwtTokenProvider
    {
        /// <summary>
        /// Generate a Jwt token for the given username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GenerateJwtToken(string userName);
    }
}
