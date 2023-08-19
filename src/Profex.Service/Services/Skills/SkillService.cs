﻿using Profex.Application.Exceptions.Skills;
using Profex.Application.Utils;
using Profex.DataAccsess.Common.Helpers;
using Profex.DataAccsess.Interfaces.Skills;
using Profex.Domain.Entities.skills;
using Profex.Persistance.Dtos.Skills;
using Profex.Service.Interfaces.Common;
using Profex.Service.Interfaces.Skills;

namespace Profex.Service.Services.Skills
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _repository;
        private readonly IPaginator _paginator;
        public SkillService(ISkillRepository skillRepository, IPaginator paginator)
        {
            this._paginator = paginator;
            this._repository = skillRepository;
        }
        public async Task<bool> CreateAsync(SkillCreateDto dto)
        {
            Skill skill = new Skill()
            {
                CategoryId = dto.CategoryId,
                Description = dto.Description,
                Title = dto.Title,
                CreatedAt = TimeHelper.GetDateTime(),
                UpdatedAt = TimeHelper.GetDateTime()
            };

            var res = await _repository.CreateAsync(skill);

            return res > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var dbResult = await _repository.DeleteAsync(id);

            return dbResult > 0;
        }

        public async Task<IList<Skill>> GetAllAsync(PaginationParams @params)
        {
            var skills = await _repository.GetAllAsync(@params);
            var count = await _repository.CountAsync();
            _paginator.Paginate(count, @params);

            return skills;
        }

        public async Task<Skill> GetByIdAsync(long id)
        {
            var skills = await _repository.GetByIdAsync(id);
            if (skills is null) throw new SkillNotFoundException();

            return skills;
        }

        public async Task<bool> UpdateAsync(long id, SkillUpdateDto dto)
        {
            var skills = await _repository.GetByIdAsync(id);
            if (skills is null) throw new SkillNotFoundException();
            skills.CategoryId = dto.CategoryId;
            skills.Title = dto.Title;
            skills.Description = dto.Description;
            skills.UpdatedAt = TimeHelper.GetDateTime();
            var dbRes = await _repository.UpdateAsync(id, skills);

            return dbRes > 0;
        }
    }
}
