using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Profex.Application.Exceptions;
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
using System.Security.Principal;
using static System.Net.Mime.MediaTypeNames;

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

    public async Task<bool> AcceptRequest(long userId, RequestAcceptDto requestAccpetDto)
    {
        // var request = await _requestRepository.AcceptRequest(userId,);
        return false;
    }

    public async Task<IList<PostWithRequestsVModel>> GetUserAllPostWithRequestAsync(long userId, PaginationParams @params)
    {
        //var requests  = await _requestRepository.GetUserAllPostWithRequestAsync(userId, @params);
        
        //List<PostWithRequestsVModel> posts = new List<PostWithRequestsVModel>();
               
        //foreach (var request in requests)
        //{
        //    var post = await _posts.GetByIdJoin(request.PostId);
        //    var postWithRequest = _mapper.Map<PostWithRequestsVModel>(post);
        //    postWithRequest.Request.Add(request);
        //    posts.Add(postWithRequest);
            
        //}

        //return posts;
        return new List<PostWithRequestsVModel>();
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

    public async Task<bool> RequestToPost(long masterId, RequestDto dto)
    {   
        var checkPost = await _requestRepository.CheckPostExixts(dto.PostId, dto.UserId);
        if (checkPost == 0) throw new PostNotFoundException();

        var check = await _requestRepository.CountPostRequestMasterCheck(masterId, dto.PostId, dto.UserId);
        if(check > 0) throw new RequestAlreadyExists();

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
