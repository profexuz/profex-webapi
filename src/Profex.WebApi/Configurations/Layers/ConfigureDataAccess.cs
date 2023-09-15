﻿using Profex.DataAccsess.Interfaces.Admins;
using Profex.DataAccsess.Interfaces.Categories;
using Profex.DataAccsess.Interfaces.Master_skills;
using Profex.DataAccsess.Interfaces.Masters;
using Profex.DataAccsess.Interfaces.Masters1;
using Profex.DataAccsess.Interfaces.Post_Images;
using Profex.DataAccsess.Interfaces.Posts;
using Profex.DataAccsess.Interfaces.Skills;
using Profex.DataAccsess.Interfaces.Users;
using Profex.DataAccsess.Interfaces.Users1;
using Profex.DataAccsess.Repositories.Adminstrators;
using Profex.DataAccsess.Repositories.Categories;
using Profex.DataAccsess.Repositories.Master_skills;
using Profex.DataAccsess.Repositories.Masters;
using Profex.DataAccsess.Repositories.Masters1;
using Profex.DataAccsess.Repositories.Post_images;
using Profex.DataAccsess.Repositories.Posts;
using Profex.DataAccsess.Repositories.Skills;
using Profex.DataAccsess.Repositories.Users;
using Profex.DataAccsess.Repositories.Users1;

namespace Profex.WebApi.Configurations.Layers
{
    public static class DataAccessConfiguration
    {
        public static void ConfigureDataAccess(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ISkillRepository, SkillRepository>();
            builder.Services.AddScoped<IMasterRepository, MasterRepository>();
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<IMaster1Repository, Master1Repository>();
            builder.Services.AddScoped<IUser1Repository, User1Repository>();
            builder.Services.AddScoped<IMasterSkillRepository, MasterSkillRepository>();
            builder.Services.AddScoped<IPostImageRepository, PostImageRepository>();
            builder.Services.AddScoped<IAdminsRepository, AdminsRepository>();
        }
    }
}
