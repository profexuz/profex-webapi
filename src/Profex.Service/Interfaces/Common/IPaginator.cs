using Profex.Application.Utils;

namespace Profex.Service.Interfaces.Common
{
    public interface IPaginator
    {
        public void Paginate(long itemsCount, PaginationParams @params);
    }
}
