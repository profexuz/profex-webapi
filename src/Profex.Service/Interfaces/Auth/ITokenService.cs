using Profex.Domain.Entities.users;

namespace Profex.Service.Interfaces.Auth
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
