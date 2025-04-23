using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AttendanceSystem.Repository.Interfaces;
using AttendanceSystem.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AttendanceSystem.Repository
{
    public class TokenRepository : ITokenRepository
    {
        protected readonly IConfiguration Configuration;

        public TokenRepository(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public string CreateJwtToken(IdentityUser user, List<string> roles)
        {
           var claims= new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role,role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration[Constants.JwtKey]));
            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(Configuration[Constants.JwtIssuer],
                                          Configuration[Constants.JwtAudience],
                                          claims,
                                          expires: DateTime.Now.AddDays(double.Parse(Configuration[Constants.JwtExpiryPeriodInDays])),
                                          signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
