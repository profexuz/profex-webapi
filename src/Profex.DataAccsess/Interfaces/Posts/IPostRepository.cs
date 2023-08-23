using Profex.Application.Utils;
using Profex.DataAccsess.Common;
using Profex.DataAccsess.ViewModels.Posts;
using Profex.Domain.Entities.posts;

namespace Profex.DataAccsess.Interfaces.Posts;

public interface IPostRepository : IRepository<Post, Post>, IGetAll<PostViewModel>, ISearchable<Post>
{
    public Task<IList<PostViewModel>> SearchAsync(string search, PaginationParams @params);
    public Task<int> SearchCountAsync(string search);
    public Task<IList<Post>> GetAllPostById(long id);
    public Task<IList<PostViewModel>> GetByIdJoin(long id);
}
