﻿using Profex.DataAccsess.Interfaces.Categories;
using Profex.DataAccsess.Interfaces.Users;
using Profex.DataAccsess.Repositories.Categories;
using Profex.DataAccsess.Repositories.Users;

namespace Profex.WebApi.Configurations.Layers
{
    public static class DataAccessConfiguration
    {
        public static void ConfigureDataAccess(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
