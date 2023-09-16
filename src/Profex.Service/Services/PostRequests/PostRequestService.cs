using Profex.Application.Utils;
using Profex.DataAccsess.Interfaces.PostRequests;
using Profex.DataAccsess.ViewModels.Posts;
using Profex.Service.Interfaces.PostRequests;

namespace Profex.Service.Services.PostRequests;

public class PostRequestService : IPostRequestService
{
    private readonly IRequestRepository _requestRepository;

    public PostRequestService(IRequestRepository requestRepository)
    {
        _requestRepository = requestRepository;
    }
    public Task<IList<PostWithRequestsVModel>> GetUserAllPostWithRequestAsync(long userId, PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public Task<PostWithRequestsVModel> GetUserPostWithRequestAsync(long userId)
    {
        throw new NotImplementedException();
    }
}
