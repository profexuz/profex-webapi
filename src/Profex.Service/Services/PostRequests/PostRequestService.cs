using AutoMapper;
using Profex.Application.Exceptions.Posts;
using Profex.Application.Exceptions.Requests;
using Profex.Application.Utils;
using Profex.DataAccsess.Common.Helpers;
using Profex.DataAccsess.Interfaces.Post_Images;
using Profex.DataAccsess.Interfaces.PostRequests;
using Profex.DataAccsess.Interfaces.Posts;
using Profex.DataAccsess.ViewModels.Posts;
using Profex.Domain.Entities.postRequests;
using Profex.Persistance.Dtos.PostRequest;
using Profex.Service.Interfaces.Common;
using Profex.Service.Interfaces.PostRequests;

namespace Profex.Service.Services.PostRequests;

public class PostRequestService : IPostRequestService
{
    private readonly IRequestRepository _requestRepository;
    private readonly IPostImageRepository _images;
    private readonly IMapper _mapper;
    private readonly IPaginator _paginator;
    private readonly IPostRepository _posts;

    public PostRequestService(IRequestRepository requestRepository,
                               IPostImageRepository images,
                                  IPaginator paginator,
                                  IPostRepository posts,
                                  IMapper mapper)
    {
        _requestRepository = requestRepository;
        _images = images;
        _mapper = mapper;
        _paginator = paginator;
        _posts = posts;

    }

    public async Task<bool> AcceptRequestAsync(long userId, RequestAcceptDto dto)
    {
        var request = await _requestRepository.AcceptRequest(userId, dto.masterId, dto.postId);
        return request;
    }

    public async Task<bool> DeleteRequestAsync(long masterId, long postId, long userId)
    {
        var check = await _requestRepository.CountPostRequestMasterCheck(masterId, postId, userId);
        if (check == 0) throw new RequestNotFoundException();

        var delete = await _requestRepository.DeleteRequestAsync(masterId, postId, userId);
        return delete > 0;
    }

    public async Task<IList<PostWithRequestsVModel>> GetMasterRequestedAllPostsAsync(long masterId, PaginationParams @params)
    {
        var requests = await _requestRepository.GetMasterRequestedAllPostsAsync(masterId, @params);
        List<PostWithRequestsVModel> posts = new List<PostWithRequestsVModel>();

        foreach (var request in requests)
        {
            if (posts.Select(x => x.Id).Contains(request.PostId))
            {
                var exsistPost = posts.FirstOrDefault(x => x.Id == request.PostId);
                exsistPost.Request.Add(request);
            }
            else
            {
                var post = await _posts.GetByIdJoin(request.PostId);
                var postWithRequest = _mapper.Map<PostWithRequestsVModel>(post);
                postWithRequest.Request.Add(request);
                posts.Add(postWithRequest);
            }
        }
       
        return posts;
    }

    public async Task<IList<PostWithRequestsVModel>> GetUserAllPostWithRequestAsync(long userId, PaginationParams @params)
    {
        var requests = await _requestRepository.GetUserAllPostWithRequestAsync(userId, @params);

        List<PostWithRequestsVModel> posts = new List<PostWithRequestsVModel>();

        foreach (var request in requests)
        {
            if (posts.Select(x => x.Id).Contains(request.PostId))
            {
                var exsistPost = posts.FirstOrDefault(x => x.Id == request.PostId);
                exsistPost.Request.Add(request);
            }
            else
            {
                var post = await _posts.GetByIdJoin(request.PostId);
                var postWithRequest = _mapper.Map<PostWithRequestsVModel>(post);
                postWithRequest.Request.Add(request);
                posts.Add(postWithRequest);
            }
        }
       
        return posts;
    }

    public async Task<PostWithRequestsVModel> GetUserPostWithRequestAsync(long userId, long postId)
    {
        var requests = await _requestRepository.GetUserPostWithRequestAsync(userId, postId);
        var post = await _posts.GetByIdJoin(postId);
        PostWithRequestsVModel postModel = new PostWithRequestsVModel();

        postModel = _mapper.Map<PostWithRequestsVModel>(post);

        postModel.Request.AddRange(requests);

        return postModel;
    }

    public async Task<bool> RequestToPostAsync(long masterId, RequestDto dto)
    {
        var checkPost = await _posts.GetByIdAsync(dto.PostId);
        if (checkPost is null) throw new PostNotFoundException();

        var check = await _requestRepository.CountPostRequestMasterCheck(masterId, dto.PostId, dto.UserId);
        if (check > 0) throw new RequestAlreadyExists();

        Request request = new Request();
        request.MasterId = masterId;
        request.PostId = dto.PostId;
        request.UserId = dto.UserId;
        request.IsAccepted = true;
        request.UpdatedAt = request.CreatedAt = TimeHelper.GetDateTime();

        var result = await _requestRepository.RequestToPost(request);

        return result > 0;
    }
}
