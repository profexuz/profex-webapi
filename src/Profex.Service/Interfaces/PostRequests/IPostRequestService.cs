using Profex.Application.Utils;
using Profex.DataAccsess.ViewModels.Posts;

namespace Profex.Service.Interfaces.PostRequests;

public interface IPostRequestService
{
    public Task<IList<PostWithRequestsVModel>> GetUserAllPostWithRequestAsync(long userId, PaginationParams @params);
    public Task<PostWithRequestsVModel> GetUserPostWithRequestAsync(long  userId);
}
