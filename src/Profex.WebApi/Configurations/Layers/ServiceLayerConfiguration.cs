using Profex.Service.Interfaces.Auth;
using Profex.Service.Interfaces.Categories;
using Profex.Service.Interfaces.Common;
using Profex.Service.Interfaces.Notifactions;
using Profex.Service.Interfaces.Skills;
using Profex.Service.Interfaces.Users;
using Profex.Service.Services.Auth;
using Profex.Service.Services.Categories;
using Profex.Service.Services.Common;
using Profex.Service.Services.Notifications;
using Profex.Service.Services.Skills;
using Profex.Service.Services.Users;

namespace Profex.WebApi.Configurations.Layers
{
    public static class ServiceLayerConfiguration
    {
        public static void ConfigureServiceLayer(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ISkillService, SkillService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<ISmsSender, SmsSender>();
        }
    }
}
