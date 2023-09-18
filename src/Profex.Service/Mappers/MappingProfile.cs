using AutoMapper;
using Profex.DataAccsess.ViewModels;
using Profex.DataAccsess.ViewModels.Posts;
using Profex.Domain.Entities.postRequests;

namespace Profex.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PostWithRequestsVModel, PostViewModel>().ReverseMap();
        CreateMap<Request, RequestViewModel>().ReverseMap();
    }
}
