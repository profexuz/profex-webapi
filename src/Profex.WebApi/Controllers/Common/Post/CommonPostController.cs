using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profex.Application.Utils;
using Profex.Service.Interfaces.Categories;
using Profex.Service.Interfaces.Posts;
using System.Security.Principal;

namespace Profex.WebApi.Controllers.Common.Post
{
    [Route("api/common/posts")]
    [ApiController]
    public class CommonPostController : ControllerBase
    {
        private readonly int defaultPageSize = 15;
        private readonly int maxPageSize = 50;

        private readonly IPostService _service;
        private readonly IIdentity _identity;
        private readonly ICategoryService _categoryService;

        public CommonPostController(IPostService Postservice, ICategoryService Categoryservice)
        {
            _service = Postservice;
            _categoryService = Categoryservice;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1, [FromQuery] int? pageSize = null)
        {
            int actualPageSize = pageSize.HasValue ? Math.Min(pageSize.Value, maxPageSize) : defaultPageSize;

            var paginationParams = new PaginationParams(page, actualPageSize);
            var posts = await _service.GetAllAsync(paginationParams);

            bool hasMore = posts.Count == paginationParams.PageSize;

            return Ok(posts);
            /*return Ok(new
            {
                Items = posts,
                HasMore = hasMore
            });*/
        }

        [HttpGet("{postId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdJoin(long postId)
        => Ok(await _service.GetByIdJoin(postId));


        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchAsync([FromQuery] string search, [FromQuery] int page = 1)
            => Ok(await _service.SearchAsync(search, new PaginationParams(page, maxPageSize)));


        [HttpGet("users/{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserAllPostAsync(long userId, int page = 1)
            => Ok(await _service.GetUserAllPostAsync(userId, new PaginationParams(page, maxPageSize)));


        [HttpGet("count")]
        [AllowAnonymous]
        public async Task<IActionResult> CountAsync()
             => Ok(await _service.CountAsync());


        [HttpGet("category/{categoryId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPostsByCategory(long categoryId, int page = 1)
        {
            var ps = await _categoryService.GetPostsByCategory(categoryId, new PaginationParams(page, maxPageSize));

            return Ok(ps);
        }


    }
}
