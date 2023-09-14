using Profex.Application.Utils;
using Profex.DataAccsess.Common;
using Profex.DataAccsess.ViewModels.Posts;
using Profex.Domain.Entities.Categories;
using Profex.Domain.Entities.skills;

namespace Profex.DataAccsess.Interfaces.Categories;

public interface ICategoryRepository : IRepository<Category, Category>, IGetAll<Category>
{
    public Task<IList<PostViewModel>> GetPostsByCategory(long category, PaginationParams @params);

    public Task<IList<Skill>> GetAllSkillByCategoryId(long categoryId, PaginationParams @params);
}
