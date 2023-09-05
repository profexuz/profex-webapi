using Microsoft.AspNetCore.Mvc.Formatters;
using Profex.Application.Exceptions.Categories;
using Profex.Application.Exceptions.Posts;
using Profex.Application.Exceptions.Users;
using Profex.Application.Utils;
using Profex.DataAccsess.Common.Helpers;
using Profex.DataAccsess.Interfaces.Categories;
using Profex.DataAccsess.Interfaces.Posts;
using Profex.DataAccsess.Interfaces.Users1;
using Profex.DataAccsess.ViewModels.Posts;
using Profex.Domain.Entities.posts;
using Profex.Persistance.Dtos.Posts;
using Profex.Service.Interfaces.Common;
using Profex.Service.Interfaces.Posts;
using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;

namespace Profex.Service.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _category;
        private readonly IPaginator _paginator;
        private readonly IUser1Repository _user;
        public PostService(
            IPostRepository postRepository,
            IPaginator paginator,
            ICategoryRepository category,
            IUser1Repository user)
        {
            this._paginator = paginator;
            this._postRepository = postRepository;
            this._category = category;
            this._user = user;
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

            post.CategoryId = dto.CategoryId;
            var js = await _category.GetByIdAsync(post.CategoryId);

            if (js == null) throw new CategoryNotFoundException();
            post.UserId = dto.UserId;
            var cs = await _user.GetByIdAsync(post.UserId);

            if (cs == null) throw new UserNotFoundException();

            var res = await _postRepository.CreateAsync(post);

            return res > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var db = await _postRepository.GetByIdAsync(id);
            if (db is null) throw new PostNotFoundException();
            var dbResult = await _postRepository.DeleteAsync(id);

            return dbResult > 0;
        }

        public async Task<IList<PostViewModel>> GetAllAsync(PaginationParams @params)
        {
            var posts = await _postRepository.GetAllAsync(@params);
            var count = await _postRepository.CountAsync();
            _paginator.Paginate(count, @params);
            return posts;
        }

        public async Task<IList<Post>> GetAllPostById(long id)
        {
            var posts = await _postRepository.GetAllPostById(id);          
            return posts;
        }

        public async Task<IList<Post>> GetByIdAsync(long id)
        {
            var posts = await _postRepository.GetByIdAsync(id);
            if (posts is null) throw new PostNotFoundException();

            return (IList<Post>)posts;
        }

        public async Task<IList<PostViewModel>> GetByIdJoin(long id)
        {
            var posts = await _postRepository.GetByIdJoin(id);
            if(posts is null) throw new PostNotFoundException();
            return posts;
        }

        public async Task<IList<PostViewModel>> SearchAsync(string search, PaginationParams @params)
        {
            var posts = await _postRepository.SearchAsync(search, @params);
            int count = await _postRepository.SearchCountAsync(search);
            return posts;
        }

        public Task<int> SearchCountAsync(string search)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(long id, PostUpdateDto dto)
        {
            var posts = await _postRepository.GetByIdAsync(id);
            if (posts is null) throw new PostNotFoundException();
            posts.CategoryId = dto.CategoryId;
            posts.UserId = dto.UserId;
            posts.Title = dto.Title;
            posts.Price = double.Parse(dto.Price.ToString());
            posts.Description = dto.Description;
            posts.Region = dto.Region;
            posts.District = dto.District;
            posts.Latidute = double.Parse(dto.Latidute.ToString());
            posts.Longitude = double.Parse(dto.Longitude.ToString());
            posts.PhoneNumber = dto.PhoneNumber;
            posts.UpdatedAt = TimeHelper.GetDateTime();
            var dbRes = await _postRepository.UpdateAsync(id, posts);

            return dbRes > 0;
        }
    }
}
