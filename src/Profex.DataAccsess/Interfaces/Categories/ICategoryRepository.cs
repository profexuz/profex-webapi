using Profex.DataAccsess.Common;
using Profex.Domain.Entities.Categories;

namespace Profex.DataAccsess.Interfaces.Categories;

public interface ICategoryRepository : IRepository<Category, Category>, IGetAll<Category>
{}
