using AutoMapper;
using Profex.DataAccsess.ViewModels.Posts;

namespace Profex.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PostWithRequestsVModel, PostViewModel>().ReverseMap();
    }
}
