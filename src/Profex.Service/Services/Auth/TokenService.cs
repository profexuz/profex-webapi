using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Profex.DataAccsess.Common.Helpers;
using Profex.Domain.Entities.users;
using Profex.Persistance.Interfaces.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Profex.Service.Services.Auth;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;

    public TokenService(IConfiguration configuration)
    {
        _config = configuration.GetSection("Jwt");
    }
    public string GenerateToken(User user)
    {
        var identityClaims = new Claim[]
        {
            new Claim("id", user.Id.ToString()),
            //new Claim("FirstName", user.First_name),
            new Claim("first_name", user.First_name.ToString()),
            new Claim("last_name", user.Last_name),
            new Claim(ClaimTypes.MobilePhone, user.Phone_number)
            //new Claim(ClaimTypes.Role, "User")
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
