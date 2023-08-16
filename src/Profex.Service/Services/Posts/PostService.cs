﻿using Profex.Application.Exceptions.Posts;
using Profex.Application.Utils;
using Profex.DataAccsess.Common.Helpers;
using Profex.DataAccsess.Interfaces.Posts;
using Profex.Domain.Entities.posts;
using Profex.Persistance.Dtos.Posts;
using Profex.Service.Interfaces.Common;
using Profex.Service.Interfaces.Posts;

namespace Profex.Service.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IPaginator _paginator;
        public PostService(IPostRepository postRepository,
            IPaginator paginator)
        {
            this._paginator = paginator;
            this._postRepository = postRepository;
        }

        public async Task<bool> CreateAsync(PostCreateDto dto)
        {
            Post post = new Post()
            {
                CategoryId = dto.CategoryId,
                UserId = dto.UserId,
                Title = dto.Title,
                Price = dto.Price,
                Description = dto.Description,
                Region = dto.Region,
                District = dto.District,
                Longitude = dto.Longitude,
                Latidute = dto.Latidute,
                PhoneNumber = dto.PhoneNumber,
                CreatedAt = TimeHelper.GetDateTime(),
                UpdatedAt = TimeHelper.GetDateTime(),
            };
            var res = await _postRepository.CreateAsync(post);
            
            return res>0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var dbResult =await _postRepository.DeleteAsync(id);
         
            return dbResult > 0;
        }

        public async Task<IList<Post>> GetAllAsync(PaginationParams @params)
        {
            var posts = await _postRepository.GetAllAsync(@params);
            var count = await _postRepository.CountAsync();
            _paginator.Paginate(count, @params);
            return posts;
        }

        public async Task<Post> GetByIdAsync(long id)
        {
            var posts = await _postRepository.GetByIdAsync(id);
            if (posts is null) throw new PostNotFoundException();

            return posts;
        }

        public async Task<bool> UpdateAsync(long id, PostUpdateDto dto)
        {
            var posts = await _postRepository.GetByIdAsync(id);
            if(posts is null) throw new PostNotFoundException();
            posts.CategoryId = dto.CategoryId;
            posts.UserId = dto.UserId;
            posts.Title = dto.Title;
            posts.Price = double.Parse(dto.Price);
            posts.Description = dto.Description;
            posts.Region = dto.Region;
            posts.District = dto.District;
            posts.Latidute = double.Parse(dto.Lattidute);
            posts.Longitude = double.Parse(dto.Longitude);
            posts.PhoneNumber = dto.PhoneNumber;
            posts.UpdatedAt = TimeHelper.GetDateTime();
            var dbRes = await _postRepository.UpdateAsync(id, posts);

            return dbRes > 0;
        }
    }
}