using Profex.Application.Utils;
using Profex.DataAccsess.ViewModels.Masters;
using Profex.Persistance.Dtos.Master1;

namespace Profex.Service.Interfaces.Master1
{
    public interface IMaster1Service
    {
        public Task<bool> DeleteAsync(long id);
        public Task<IList<MasterViewModel>> GetAllAsync(PaginationParams @params);
        public Task<MasterViewModel> GetByIdAsync(long id);
        public Task<bool> UpdateAsync(long id, Master1UpdateDto dto);
    }
}
