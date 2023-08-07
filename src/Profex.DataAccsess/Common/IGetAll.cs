using Profex.Application.Utils;

namespace Profex.DataAccsess.Common
{
    public interface IGetAll<TModel>
    {
        public Task<IList<TModel>> GetAllAsync(PaginationParams @params);
    }
}
