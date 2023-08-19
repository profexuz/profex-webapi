using Profex.Application.Utils;
using Profex.Domain.Entities.master_skills;
using Profex.Persistance.Dtos.MasterSkill;

namespace Profex.Service.Interfaces.MasterSkill;

public interface IMasterSkillService
{
    public Task<bool> CreateAsync(MasterSkillCreateDto dto);
    public Task<bool> DeleteAsync(long id);
    public Task<IList<Master_skill>> GetAllAsync(PaginationParams @params);
    public Task<Master_skill> GetByIdAsync(long id);
    public Task<bool> UpdateAsync(long id, MasterSkillUpdateDto dto);
}
