using Profex.DataAccsess.Repositories.Post_images;
using Profex.Service.Interfaces.Auth;
using Profex.Service.Interfaces.Categories;
using Profex.Service.Interfaces.Common;
using Profex.Service.Interfaces.Master1;
using Profex.Service.Interfaces.MasterAuth;
using Profex.Service.Interfaces.MasterSkill;
using Profex.Service.Interfaces.Notifactions;
using Profex.Service.Interfaces.PostImages;
using Profex.Service.Interfaces.Posts;
using Profex.Service.Interfaces.Skills;
using Profex.Service.Interfaces.User1;
using Profex.Service.Interfaces.Users;
using Profex.Service.Services.Auth;
using Profex.Service.Services.Categories;
using Profex.Service.Services.Categories.Layers;
using Profex.Service.Services.Common;
using Profex.Service.Services.Master1;
using Profex.Service.Services.MasterAuth;
using Profex.Service.Services.MasterSkill;
using Profex.Service.Services.Notifications;
using Profex.Service.Services.PostImages;
using Profex.Service.Services.Posts;
using Profex.Service.Services.Skills;
using Profex.Service.Services.User1;
using Profex.Service.Services.Users;

namespace Profex.WebApi.Configurations.Layers
{
    public static class ServiceLayerConfiguration
    {
        public static void ConfigureServiceLayer(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ISkillService, SkillService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IPaginator, Paginator>();
            builder.Services.AddScoped<IAuthMasterService, AuthMasterService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<ISmsSender, SmsSender>();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<IMaster1Service, Master1Service>();
            builder.Services.AddScoped<IUser1Service, User1Service>();
            builder.Services.AddScoped<IMasterSkillService, MasterSkillService>();
            builder.Services.AddScoped<IPostImagesService, PostImagesService>();
        }
    }
}
