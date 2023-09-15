

using Profex.Application.Utils;
using Profex.DataAccsess.Common;
using Profex.DataAccsess.ViewModels.Posts;
using Profex.Domain.Entities.postRequests;

namespace Profex.DataAccsess.Interfaces.PostRequests;

public interface IRequestRepository : IRepository<Request, Request>, IGetAll<Request>
{
    public Task<IList<Request>> GetPostRequestsAsync(long userId, PaginationParams @paramms);
    public Task<long> CountPostRequestMasterCheck(long masterId, long postId, long userId);
    public Task<long> CountPostRequestUserPost(long postId);
}
