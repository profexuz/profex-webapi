using Profex.DataAccsess.Common;
using Profex.Domain.Entities.master_skills;

namespace Profex.DataAccsess.Interfaces.Master_skills
{
    public interface IMasterSkillRepository : IRepository<Master_skill, Master_skill>, IGetAll<Master_skill>, ISearchable<Master_skill>
    {
        public Task<IList<Master_skill>> GetMasterAllSkillAsync(long masterId);
        public Task<Master_skill> GetMasterSkillIdAsync(long masterId, long skillId);
        public Task<int> DeleteMasterSkillIdAsync(long masterId, long skillId);

    }
}
