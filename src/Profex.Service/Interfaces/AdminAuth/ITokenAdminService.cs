using Profex.Domain.Entities.users;

namespace Profex.Service.Interfaces.AdminAuth
{
    public interface ITokenAdminService
    {
        public string GenerateToken(User user);
    }
}
