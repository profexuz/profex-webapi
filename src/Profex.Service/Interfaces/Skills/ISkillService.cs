using Profex.Application.Utils;
using Profex.Domain.Entities.skills;
using Profex.Persistance.Dtos.Skills;

namespace Profex.Service.Interfaces.Skills
{
    public interface ISkillService
    {
        public Task<bool> CreateAsync(SkillCreateDto dto);
        public Task<bool> DeleteAsync(long id);
        public Task<IList<Skill>> GetAllAsync(PaginationParams @params);
        public Task<Skill> GetByIdAsync(long id);
        public Task<bool> UpdateAsync(long id, SkillUpdateDto dto);
    }
}
