using Profex.Application.Utils;
using Profex.DataAccsess.ViewModels.Masters;
using Profex.DataAccsess.ViewModels.Skills;
using Profex.Domain.Entities.master_skills;
using Profex.Persistance.Dtos.Master1;

namespace Profex.Service.Interfaces.Master1
{
    public interface IMaster1Service
    {
        public Task<IList<Master_skill>> SortBySkillId(long skillId);
        public Task<IList<MasterViewModel>> SearchAsync(string search, PaginationParams @params);
        public Task<int> SearchCountAsync(string search);
        public Task<bool> DeleteAsync(long id);
        public Task<bool> DeleteMasterAsync();
        public Task<IList<MasterViewModel>> GetAllAsync(PaginationParams @params);
        public Task<MasterViewModel> GetByIdAsync(long id);
        public Task<bool> UpdateAsync(long id, Master1UpdateDto dto);
        public Task<IList<UserSkillViewModel>> GetMasterSkillById(long masterId);
    }
}
