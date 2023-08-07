using Profex.Domain.Entities.users;

namespace Profex.Persistance.Interfaces.Auth
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
