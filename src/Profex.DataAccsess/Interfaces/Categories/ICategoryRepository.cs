using Profex.DataAccsess.Common;
using Profex.Domain.Entities.Categories;
using Profex.Domain.Entities.posts;

namespace Profex.DataAccsess.Interfaces.Categories;

public interface ICategoryRepository : IRepository<Category, Category>, IGetAll<Category>
{
    public Task<IList<Post>> GetPostsByCategory(long category);
}
