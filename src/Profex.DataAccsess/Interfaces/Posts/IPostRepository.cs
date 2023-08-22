﻿using Profex.Application.Utils;
using Profex.DataAccsess.Common;
using Profex.Domain.Entities.posts;

namespace Profex.DataAccsess.Interfaces.Posts;

public interface IPostRepository : IRepository<Post, Post>, IGetAll<Post>, ISearchable<Post>
{
    public Task<IList<Post>> SearchAsync(string search, PaginationParams @params);
    public Task<int> SearchCountAsync(string search);
    public Task<IList<Post>> GetAllPostById(long id);
}
