using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Profex.DataAccsess.Common.Helpers;
using Profex.Domain.Entities.admins;
using Profex.Domain.Entities.users;
using Profex.Service.Interfaces.AdminAuth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Profex.Service.Services.AdminAuth
{
    public class TokenAdminService : ITokenAdminService
    {
        private readonly IConfiguration _config;
        public TokenAdminService(IConfiguration configuration)
        {
            _config = configuration.GetSection("Jwt");
        }
        public string GenerateToken(User user)
        {
            var identityClaims = new Claim[]
        {
            new Claim("Id", user.Id.ToString()),
            new Claim("FirstName", user.FirstName),
            //new Claim("LastName", user.LastName),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
            new Claim(ClaimTypes.Role, "Admin")
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["SecurityKey"]!));
            var keyCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            int expiresHours = int.Parse(_config["Lifetime"]!);
            var token = new JwtSecurityToken(
                issuer: _config["Issuer"],
                audience: _config["Audience"],
                claims: identityClaims,
                expires: TimeHelper.GetDateTime().AddHours(expiresHours),
                signingCredentials: keyCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateToken(Admin admin)
        {
            var identityClaims = new Claim[]
  {
            new Claim("Id", admin.Id.ToString()),
            new Claim("FirstName", admin.FirstName),
            new Claim("LastName", admin.LastName),
            new Claim(ClaimTypes.MobilePhone, admin.PhoneNumber),
            new Claim(ClaimTypes.Role, "Admin")
  };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["SecurityKey"]!));
            var keyCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            int expiresHours = int.Parse(_config["Lifetime"]!);
            var token = new JwtSecurityToken(
                issuer: _config["Issuer"],
                audience: _config["Audience"],
                claims: identityClaims,
                expires: TimeHelper.GetDateTime().AddHours(expiresHours),
                signingCredentials: keyCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
