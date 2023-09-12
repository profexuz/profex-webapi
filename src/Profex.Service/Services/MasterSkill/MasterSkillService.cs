using Profex.Application.Exceptions;
using Profex.Application.Exceptions.Masters;
using Profex.Application.Exceptions.MasterSkills;
using Profex.Application.Exceptions.Skills;
using Profex.Application.Utils;
using Profex.DataAccsess.Common.Helpers;
using Profex.DataAccsess.Interfaces.Master_skills;
using Profex.DataAccsess.Interfaces.Masters;
using Profex.DataAccsess.Interfaces.Skills;
using Profex.Domain.Entities.master_skills;
using Profex.Persistance.Dtos.MasterSkill;
using Profex.Service.Interfaces.Common;
using Profex.Service.Interfaces.Identity;
using Profex.Service.Interfaces.MasterSkill;
using System.Security.Principal;

namespace Profex.Service.Services.MasterSkill;

public class MasterSkillService : IMasterSkillService
{
    private readonly IMasterSkillRepository _repository;
    private readonly IMasterRepository _master;
    private readonly ISkillRepository _skill;
    private readonly IIdentityService _identity;
    private readonly IMasterSkillRepository _masterSkill;
    private readonly IPaginator _paginator;
    public MasterSkillService(IMasterSkillRepository repository, IPaginator paginator, 
        IMasterRepository masterRepository, ISkillRepository skill,
        IIdentityService identity, IMasterSkillRepository masterSkill)
    {
        this._repository = repository;
        this._paginator = paginator;
        this._master = masterRepository;
        this._skill = skill;
        this._identity = identity;
        this._masterSkill = masterSkill;
    }
    public async Task<bool> CreateAsync(MasterSkillCreateDto dto)
    {

        Master_skill ms = new Master_skill()
        {
            MasterId = _identity.UserId,
            
            SkillId = dto.SkillId,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };

        ms.MasterId =_identity.UserId;
        ms.SkillId = dto.SkillId;
        var rap = await _master.GetByIdAsync(ms.MasterId);
        var skills = await _masterSkill.GetMasterAllSkillAsync(_identity.UserId);
        if (rap == null) throw new MasterNotFoundException();
        ms.SkillId = dto.SkillId;
        var rp = await _skill.GetByIdAsync(ms.SkillId);
        if(rp==null) throw new SkillNotFoundException();
        
        foreach (var skill in skills) 
        { 
            if(skill.SkillId == dto.SkillId)
            {
                throw new MasterSkillAlreadyExists();
            }
        }
        
        var res = await _repository.CreateAsync(ms);

        return res > 0;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var natija = await _repository.GetByIdAsync(id);
        if (natija == null) throw new MasterSkilNotFoundException();
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
        if (ms is null) throw new MasterSkilNotFoundException();
        ms.MasterId = dto.MasterId;
        ms.SkillId = dto.SkillId;
        ms.UpdatedAt = TimeHelper.GetDateTime();
        var Res = await _repository.UpdateAsync(id, ms);

        return Res > 0;
    }
}
