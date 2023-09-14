using Profex.Application.Exceptions.Categories;
using Profex.Application.Exceptions.Posts;
using Profex.Application.Utils;
using Profex.DataAccsess.Common.Helpers;
using Profex.DataAccsess.Interfaces.Categories;
using Profex.DataAccsess.Interfaces.Post_Images;
using Profex.DataAccsess.ViewModels.Posts;
using Profex.Domain.Entities.Categories;
using Profex.Domain.Entities.posts;
using Profex.Domain.Entities.skills;
using Profex.Persistance.Dtos.Categories;
using Profex.Service.Interfaces.Categories;
using Profex.Service.Interfaces.Common;

namespace Profex.Service.Services.Categories;
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IPaginator _paginator;
    private readonly IPostImageRepository _images;

    public CategoryService(ICategoryRepository categoryRepository,
                            IPostImageRepository images,
                            IPaginator paginator)
    {
        this._repository = categoryRepository;
        this._paginator = paginator;
        this._images = images;
    }

    public async Task<long> CountAsync() => await _repository.CountAsync();

    public async Task<bool> CreateAsync(CategoryCreateDto dto)
    {
        Category category = new Category()
        {
            Name = dto.Name,
            Description = dto.Description,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };

        var res = await _repository.CreateAsync(category);

        return res > 0;
    }

    public async Task<bool> DeleteAsync(long categoryId)
    {
        var natija = await _repository.GetByIdAsync(categoryId);
        if (natija == null) throw new CategoryNotFoundException();

        var dbResult = await _repository.DeleteAsync(categoryId);
        
        return dbResult > 0;
    }

    public async Task<IList<Category>> GetAllAsync(PaginationParams @params)
    {
        var categories = await _repository.GetAllAsync(@params);
        var count = await _repository.CountAsync();
        _paginator.Paginate(count, @params);

        return categories;
    }

    public async Task<IList<Skill>> GetAllSkillByCategoryId(long categoryId, PaginationParams @params)
    {
        var natija = await _repository.GetByIdAsync(categoryId);
        if (natija == null) throw new CategoryNotFoundException();
        var skills = await _repository.GetAllSkillByCategoryId(categoryId,  @params);

        return skills;
        
    }

   

    public async Task<Category> GetByIdAsync(long categoryId)
    {
        var category = await _repository.GetByIdAsync(categoryId);
        if (category is null) throw new CategoryNotFoundException();

        return category;
    }

    public async Task<IList<PostViewModel>> GetPostsByCategory(long category, PaginationParams @params)
    {
        var posts = await _repository.GetPostsByCategory(category, @params);
        foreach (var post in posts)
        {
            var imagePaths = await _images.GetByPostIdAsync(post.Id);
            post.Images.AddRange(imagePaths);
        }
        if (posts  is null) throw new PostNotFoundException();
        return posts;

    }

  

    public async Task<bool> UpdateAsync(long categoryId, CategoryUpdateDto dto)
    {
        var category = await _repository.GetByIdAsync(categoryId);
        if (category is null) throw new CategoryNotFoundException();
        category.Name = dto.Name;
        category.Description = dto.Description;
        category.UpdatedAt = TimeHelper.GetDateTime();
        var dbres = await _repository.UpdateAsync(categoryId, category);

        return dbres > 0;
    }
}
