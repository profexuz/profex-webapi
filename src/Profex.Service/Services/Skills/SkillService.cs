using Profex.Application.Exceptions.Categories;
using Profex.Application.Exceptions.Skills;
using Profex.Application.Utils;
using Profex.DataAccsess.Common.Helpers;
using Profex.DataAccsess.Interfaces.Categories;
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
        private readonly ICategoryRepository _category;
        
        public SkillService(ISkillRepository skillRepository, IPaginator paginator,ICategoryRepository categoryRepository)
        {
            this._paginator = paginator;
            this._repository = skillRepository;
            this._category = categoryRepository;
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
            skill.CategoryId = dto.CategoryId;
            var js = _category.GetByIdAsync(skill.CategoryId);
            if (js == null) throw new CategoryNotFoundException();
            
            var res = await _repository.CreateAsync(skill);

            return res > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var natija = await _repository.GetByIdAsync(id);
            if (natija != null) throw new SkillNotFoundException();
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
