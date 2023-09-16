using Profex.Domain.Entities.admins;
using Profex.Domain.Entities.users;

namespace Profex.Service.Interfaces.AdminAuth
{
    public interface ITokenAdminService
    {
        public string GenerateToken(Admin admin);
        public string GenerateToken(User user);
    }
}
