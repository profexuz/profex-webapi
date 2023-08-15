using Profex.Domain.Entities.masters;

namespace Profex.Service.Interfaces.MasterAuth
{
    public interface ITokenMasterService
    {
        public string GenerateToken(Master master);
    }
}
