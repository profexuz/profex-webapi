using Profex.Application.Utils;
using Profex.DataAccsess.ViewModels.Masters;
using Profex.DataAccsess.ViewModels.Skills;
using Profex.Domain.Entities.master_skills;
using Profex.Persistance.Dtos.Masters;

namespace Profex.Service.Interfaces.Masters
{
    public interface IMasterService
    {
        public Task<IList<Master_skill>> SortBySkillId(long skillId);
        public Task<IList<MasterViewModel>> SearchAsync(string search, PaginationParams @params);
        public Task<int> SearchCountAsync(string search);
        public Task<bool> DeleteAsync(long id);
        public Task<bool> DeleteMasterAsync();
        public Task<IList<MasterWithSkillsModel>> GetAllAsync(PaginationParams @params);
        public Task<MasterViewModel> GetByIdAsync(long id);
        public Task<bool> UpdateAsync(long id,  MasterUpdateDto dto);
        public Task<IList<UserSkillViewModel>> GetMasterSkillById(long masterId);
        public Task<MasterWithSkillsModel> GetMasterWithSkillsAsync(long masterId);
    }
}
