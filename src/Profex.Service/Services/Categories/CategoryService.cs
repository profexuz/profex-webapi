using Microsoft.Extensions.Caching.Memory;
using Profex.Application.Exceptions.Users;
using Profex.Application.Utils;
using Profex.DataAccsess.Common.Helpers;
using Profex.DataAccsess.Interfaces.Categories;
using Profex.Domain.Entities.Categories;
using Profex.Persistance.Dtos.Categories;
using Profex.Persistance.Interfaces.Categories;
using Profex.Persistance.Interfaces.Common;

namespace Profex.Service.Services.Categories;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    //private readonly IFileService _fileService;
    //private readonly IMemoryCache _memoryCache;
    private readonly IPaginator _paginator;
    public CategoryService(ICategoryRepository categoryRepository,
        //IFileService fileService,
        //IMemoryCache memoryCache,
        IPaginator paginator)
    {
        this._repository = categoryRepository;
        //this._fileService = fileService;
        //this._memoryCache = memoryCache;
        this._paginator = paginator;
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

    public async Task<Category> GetByIdAsync(long categoryId)
    {
        var category = await _repository.GetByIdAsync(categoryId);
        if (category is null) throw new UserNotFoundException();

        return category;
    }

    public async Task<bool> UpdateAsync(long categoryId, CategoryUpdateDto dto)
    {
        var categroy = await _repository.GetByIdAsync(categoryId);
        if(categroy is null) throw new UserNotFoundException();
        categroy.Name = dto.Name;
        categroy.Description=dto.Description;
        categroy.UpdatedAt = TimeHelper.GetDateTime();
        var dbres = await _repository.UpdateAsync(categoryId, categroy);

        return dbres > 0;
    }
}
