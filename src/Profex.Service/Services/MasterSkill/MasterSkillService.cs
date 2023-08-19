using Profex.Application.Exceptions.MasterSkills;
using Profex.Application.Utils;
using Profex.DataAccsess.Common.Helpers;
using Profex.DataAccsess.Interfaces.Master_skills;
using Profex.Domain.Entities.master_skills;
using Profex.Persistance.Dtos.MasterSkill;
using Profex.Service.Interfaces.Common;
using Profex.Service.Interfaces.MasterSkill;

namespace Profex.Service.Services.MasterSkill;

public class MasterSkillService : IMasterSkillService
{
    private readonly IMasterSkillRepository _repository;
    private readonly IPaginator _paginator;
    public MasterSkillService(IMasterSkillRepository repository, IPaginator paginator)
    {
        this._repository = repository;
        this._paginator = paginator;
    }
    public async Task<bool> CreateAsync(MasterSkillCreateDto dto)
    {
        Master_skill ms = new Master_skill()
        {
            MasterId = dto.MasterId,
            SkillId = dto.SkillId,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };
      

        var res = await _repository.CreateAsync(ms);

        return res > 0;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var dbResult = await _repository.DeleteAsync(id);

        return dbResult > 0;
    }

    public async Task<IList<Master_skill>> GetAllAsync(PaginationParams @params)
    {
        var mss = await _repository.GetAllAsync(@params);
        var count = await _repository.CountAsync();
        _paginator.Paginate(count, @params);

        return mss;
    }

    public async Task<Master_skill> GetByIdAsync(long id)
    {
        var ms = await _repository.GetByIdAsync(id);
        if (ms is null) throw new MasterSkilNotFoundException();

        return ms;
    }

    public async Task<bool> UpdateAsync(long id, MasterSkillUpdateDto dto)
    {
        var ms = await _repository.GetByIdAsync(id);
        if(ms is null) throw new MasterSkilNotFoundException();
        ms.MasterId = dto.MasterId;
        ms.SkillId = dto.SkillId;
        ms.UpdatedAt = TimeHelper.GetDateTime();
        var Res = await _repository.UpdateAsync(id, ms);
        
        return Res > 0;
    }
}
