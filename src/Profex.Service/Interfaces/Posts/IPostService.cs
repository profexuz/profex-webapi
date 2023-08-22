using Profex.Application.Utils;
using Profex.Domain.Entities.posts;
using Profex.Persistance.Dtos.Posts;

namespace Profex.Service.Interfaces.Posts;

public interface IPostService
{
    public Task<bool> CreateAsync(PostCreateDto dto);
    public Task<bool> DeleteAsync(long id);
    public Task<IList<Post>> GetAllAsync(PaginationParams @params);
    public Task<Post> GetByIdAsync(long id);
    public Task<bool> UpdateAsync(long id, PostUpdateDto dto);
    public Task<IList<Post>> SearchAsync(string search, PaginationParams @params);
    public Task<int> SearchCountAsync(string search);
    public Task<IList<Post>> GetAllPostById(long id);
}
