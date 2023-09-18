using Profex.Application.Utils;
using Profex.DataAccsess.ViewModels;
using Profex.Domain.Entities.postRequests;

namespace Profex.DataAccsess.Interfaces.PostRequests;

public interface IRequestRepository
{
    public Task<IList<Request>> GetUserAllPostWithRequestAsync(long userId, PaginationParams @paramms);
    public Task<IList<Request>> GetMasterRequestedAllPostsAsync(long masterId,  PaginationParams @params);
    public Task<IList<Request>> GetUserPostWithRequestAsync(long userId, long postId);
    public Task<int> CountPostRequestMasterCheck(long masterId, long postId, long userId);
    public Task<int> CheckPostExixts(long postId, long userId);
    public Task<long> CountPostRequestUserPost(long postId);
    public Task<long> RequestToPost(Request request);
    public Task<bool> AcceptRequest( long userId, long masterId, long postId);
    public Task<long> DeleteRequestAsync(long masterId, long postId, long user_id);
}
