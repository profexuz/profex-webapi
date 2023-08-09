using Profex.DataAccsess.Common;
using Profex.Domain.Entities.masters;

namespace Profex.DataAccsess.Interfaces.Master_skills
{
    public interface IMasterSkills : IRepository<Master,Master>, IGetAll<Master>, ISearchable<Master>
    {}
}
