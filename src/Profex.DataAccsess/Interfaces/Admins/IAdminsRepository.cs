using Profex.DataAccsess.Common;
using Profex.DataAccsess.ViewModels.Adminstrators;
using Profex.Domain.Entities.admins;

namespace Profex.DataAccsess.Interfaces.Admins;

public interface IAdminsRepository : IRepository<Admin, AdminstratorsViewModel>, IGetAll<AdminstratorsViewModel>
{
    public Task<AdminstratorsViewModel?> GetByPhoneAsync(string phone);
}
