using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profex.Application.Utils;
using Profex.Service.Interfaces.Posts;

namespace Profex.WebApi.Controllers.Common.Post
{
    [Route("api/common/post")]
    [ApiController]
    public class CommonPostController : ControllerBase
    {
        private readonly int maxPageSize = 30;
        private readonly IPostService _service;

        public CommonPostController(IPostService Postservice)
        {
            _service = Postservice;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
            => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

        [HttpGet("{postId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(long postId)
        => Ok(await _service.GetByIdAsync(postId));

        [HttpGet("join/{postId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdJoin(long postId)
        => Ok(await _service.GetByIdJoin(postId));

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchAsync([FromQuery] string search, [FromQuery] int page = 1)
            => Ok(await _service.SearchAsync(search, new PaginationParams(page, maxPageSize)));
        
        [HttpGet("postsById")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllPostsById([FromQuery] long id)
            => Ok(await _service.GetAllPostById(id));
    }
}
