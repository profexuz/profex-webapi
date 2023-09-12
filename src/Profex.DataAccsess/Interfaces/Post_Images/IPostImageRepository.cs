﻿using Profex.DataAccsess.Common;
using Profex.Domain.Entities.post_images;

namespace Profex.DataAccsess.Interfaces.Post_Images
{
    public interface IPostImageRepository : IRepository<Post_image, Post_image>, IGetAll<Post_image>
    {
        public Task<IList<Post_image>> GetByPostIdAsync(long id);

        public Task<int> CountPostImagesAsync(long postId);
        
    }
}
