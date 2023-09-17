using Profex.Application.Utils;
using Profex.DataAccsess.ViewModels.Posts;
using Profex.Persistance.Dtos.PostRequest;

namespace Profex.Service.Interfaces.PostRequests;

public interface IPostRequestService
{
    public Task<IList<PostWithRequestsVModel>> GetUserAllPostWithRequestAsync(long userId, PaginationParams @params);
    public Task<PostWithRequestsVModel> GetUserPostWithRequestAsync(long  userId, long postId);

    public Task<bool> RequestToPost(long masterId, RequestDto requestDto);
}
