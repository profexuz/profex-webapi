using Profex.Application.Utils;

namespace Profex.Persistance.Interfaces.Common
{
    public interface IPaginator
    {
        public void Paginate(long itemsCount, PaginationParams @params);
    }
}
