using Profex.Persistance.Interfaces.Auth;
using Profex.Persistance.Interfaces.Categories;
using Profex.Persistance.Interfaces.Common;
using Profex.Persistance.Interfaces.Notifications;
using Profex.Service.Services.Auth;
using Profex.Service.Services.Categories;
using Profex.Service.Services.Common;
using Profex.Service.Services.Notifications;

namespace Profex.WebApi.Configurations.Layers
{
    public static class ServiceLayerConfiguration
    {
        public static void ConfigureServiceLayer(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<ISmsSender, SmsSender>();
        }
    }
}
