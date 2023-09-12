using Profex.Application.Exceptions.Masters;
using Profex.Application.Exceptions.MasterSkills;
using Profex.Application.Exceptions.Skills;
using Profex.Application.Utils;
using Profex.DataAccsess.Common.Helpers;
using Profex.DataAccsess.Interfaces.Master_skills;
using Profex.DataAccsess.Interfaces.Masters1;
using Profex.DataAccsess.Interfaces.Skills;
using Profex.DataAccsess.ViewModels.Masters;
using Profex.DataAccsess.ViewModels.Skills;
using Profex.Domain.Entities.master_skills;
using Profex.Domain.Entities.masters;
using Profex.Domain.Entities.skills;
using Profex.Persistance.Dtos.Master1;
using Profex.Service.Interfaces.Common;
using Profex.Service.Interfaces.Identity;
using Profex.Service.Interfaces.Master1;
using System.Diagnostics.Metrics;

namespace Profex.Service.Services.Master1
{
    public class Master1Service : IMaster1Service
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IMaster1Repository _repository;
        private readonly IPaginator _paginator;
        private IFileService _fileService;
        private readonly IIdentityService _identity;
        private readonly IMasterSkillRepository _skillrepo;

        public Master1Service(IMaster1Repository master1Repository, IPaginator paginator, 
                                IFileService fileService, IIdentityService identity,
                                IMasterSkillRepository skillrepo,
                                ISkillRepository skillRepository)
        {

            this._skillRepository = skillRepository;
            this._repository = master1Repository;
            this._paginator = paginator;
            this._fileService = fileService;
            this._identity = identity;
            this._skillrepo = skillrepo;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var natjia = await _repository.GetByIdAsync(id);
            if (natjia == null) throw new MasterNotFoundException();

            var dbResult = await _repository.DeleteAsync(id);
            
            return dbResult > 0;
        }

        public async Task<bool> DeleteMasterAsync()
        {
            var natjia = await _repository.GetByIdAsync(_identity.UserId);
            if (natjia == null) throw new MasterNotFoundException();

            var dbResult = await _repository.DeleteAsync(_identity.UserId);

            return dbResult > 0;
        }

        public async Task<IList<MasterWithSkillsModel>> GetAllAsync(PaginationParams @params)
        {
            var masters1 = await _repository.GetAllAsync(@params);
            List<MasterWithSkillsModel> MasterWithSkillList = new();
          
           
            foreach (var master in masters1)
            {
                var masterWithSkill = new MasterWithSkillsModel()
                {   
                    Id = master.Id,
                    FirstName = master.FirstName,
                    LastName = master.LastName,
                    ImagePath = master.ImagePath,
                    IsFree = master.IsFree,
                    PhoneNumber = master.PhoneNumber,
                    CreatedAt = master.CreatedAt,
                    UpdatedAt = master.UpdatedAt
                };
                var skills_id = await _skillrepo.GetMasterAllSkillAsync(master.Id);
                foreach (var id in skills_id)
                {
                    var skill = await _skillRepository.GetByIdAsync(id.SkillId);
                    if (skill is not null)
                    {
                        masterWithSkill.MasterSkills.Add(skill);
                    }
                }
                MasterWithSkillList.Add(masterWithSkill);
            }
            var count = await _repository.CountAsync();
            _paginator.Paginate(count, @params);

            return MasterWithSkillList;
        }
        public async Task<MasterWithSkillsModel> GetMasterWithSkillsAsync(long masterId)
        {
            var master = await _repository.GetByIdAsync(masterId);
            if (master is null) throw new MasterNotFoundException();
            var skills_id = await _skillrepo.GetMasterAllSkillAsync(masterId);
            var masterWithSkill = new MasterWithSkillsModel()
            {
                Id = masterId,
                FirstName = master.FirstName,
                LastName = master.LastName,
                ImagePath = master.ImagePath,
                IsFree = master.IsFree,
                PhoneNumber = master.PhoneNumber,
                CreatedAt = master.CreatedAt,
                UpdatedAt = master.UpdatedAt
            };
           // masterWithSkill.MasterSkills = new List<Skill>();
            foreach (var id in skills_id)
            {
                    var skill = await _skillRepository.GetByIdAsync(id.SkillId);
                if(skill is not null)
                {
                    masterWithSkill.MasterSkills.Add(skill);
                }
                
            }
            

            return masterWithSkill;
        }

        public async Task<MasterViewModel> GetByIdAsync(long id)
        {
            var master = await _repository.GetByIdAsync(id);
            if (master is null) throw new MasterNotFoundException();

            else return master;
        }

        public async Task<IList<UserSkillViewModel>> GetMasterSkillById(long masterId)
        {
            var masters = await _repository.GetMasterSkillById(masterId);
            if(masters is null) throw new SkillNotFoundException();
            return masters;
        }
            
        

        public async Task<IList<MasterViewModel>> SearchAsync(string search, PaginationParams @params)
        {
            var masters =await _repository.SearchAsync(search, @params);
            if(masters is null) throw new MasterNotFoundException();
            return masters;
        }

        public Task<int> SearchCountAsync(string search)
        {
            throw new NotImplementedException();

        }

        public async Task<IList<Master_skill>> SortBySkillId(long skillId)
        {
            var posts = await _repository.SortBySkillId(skillId);
            if (posts is null) throw new MasterSkilNotFoundException();
            return posts;
        }

        public async Task<bool> UpdateAsync(long id, Master1UpdateDto dto)
        {
            var master1 = await _repository.GetByIdAsync(id);
            if (master1 is null) throw new MasterNotFoundException();
            master1.FirstName = dto.FirstName;
            master1.LastName = dto.LastName;
            master1.PhoneNumber = dto.PhoneNumber;
            master1.IsFree = dto.IsFree;
            if (dto.ImagePath is not null)
            {
                string newImagePath = await _fileService.UploadImageAsync(dto.ImagePath);
                master1.ImagePath = newImagePath;
            }
            master1.UpdatedAt = TimeHelper.GetDateTime();
            var dbRes = await _repository.UpdateAsync(id, master1);

            return dbRes > 0;
        }
    }
}
