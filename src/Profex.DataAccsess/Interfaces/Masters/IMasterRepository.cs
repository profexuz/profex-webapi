using Profex.Application.Utils;
using Profex.DataAccsess.Common;
using Profex.Domain.Entities.masters;

namespace Profex.DataAccsess.Interfaces.Masters
{
    public interface IMasterRepository : IRepository<Master, Master>, IGetAll<Master>, ISearchable<Master>
    {

        public Task<IList<Master>> SearchAsync(string search, PaginationParams @params);
        public Task<int> SearchCountAsync(string search);


    }
}
