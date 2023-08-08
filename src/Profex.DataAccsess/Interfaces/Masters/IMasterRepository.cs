using Profex.Application.Utils;
using Profex.DataAccsess.Common;
using Profex.DataAccsess.ViewModels.Masters;
using Profex.Domain.Entities.masters;

namespace Profex.DataAccsess.Interfaces.Masters;

public interface IMasterRepository : IRepository<Master, MasterViewModel>, IGetAll<MasterViewModel>, ISearchable<MasterViewModel>
{
    public Task<IList<MasterViewModel>> SearchAsync(string search, PaginationParams @params);
    public Task<int> SearchCountAsync(string search);
}
