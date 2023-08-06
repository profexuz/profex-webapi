using Profex.DataAccsess.Common;
using Profex.Domain.Entities.skills;

namespace Profex.DataAccsess.Interfaces.Skills;

public interface ISkillRepository : IRepository<Skill,Skill>, IGetAll<Skill>  
{

}
