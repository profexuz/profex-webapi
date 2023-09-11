using Profex.Application.Utils;
using Profex.DataAccsess.Common;
using Profex.DataAccsess.ViewModels.Masters;
using Profex.DataAccsess.ViewModels.Skills;
using Profex.Domain.Entities.master_skills;
using Profex.Domain.Entities.masters;
using static Dapper.SqlMapper;

namespace Profex.DataAccsess.Interfaces.Masters1
{
    public interface IMaster1Repository : IRepository<Master, MasterViewModel>, IGetAll<MasterViewModel>, ISearchable<MasterViewModel>
    {
        public Task<IList<Master_skill>> SortBySkillId(long  skillId);
        public Task<IList<MasterViewModel>> SearchAsync(string search, PaginationParams @params);
        public Task<int> SearchCountAsync(string search);
        public Task<Master?> GetByPhoneAsync(string phone);
        public Task<IList<UserSkillViewModel>> GetMasterSkillById(long masterId);
        public Task<int> UpdateAsync(long id, MasterViewModel entity);
       
    }
}
