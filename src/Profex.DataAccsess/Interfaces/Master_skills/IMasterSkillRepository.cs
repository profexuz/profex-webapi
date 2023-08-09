using Profex.DataAccsess.Common;
using Profex.Domain.Entities.master_skills;
using Profex.Domain.Entities.masters;

namespace Profex.DataAccsess.Interfaces.Master_skills
{
    public interface IMasterSkillRepository : IRepository<Master_skill, Master_skill>, IGetAll<Master_skill>, ISearchable<Master_skill>
    {}
}
