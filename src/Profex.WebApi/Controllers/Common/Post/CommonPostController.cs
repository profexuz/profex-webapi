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
        private readonly int maxPageSize = 6;
        private readonly IPostService _service;

        public CommonPostController(IPostService Postservice)
        {
            _service = Postservice;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
            => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

        
        [HttpGet("byId/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdJoin(long id)
        => Ok(await _service.GetByIdJoin(id));

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchAsync([FromQuery] string search, [FromQuery] int page = 1)
            => Ok(await _service.SearchAsync(search, new PaginationParams(page, maxPageSize)));
        
        [HttpGet("user/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserAllPostAsync( long id, int page = 1 )
            => Ok(await _service.GetUserAllPostAsync(id, new PaginationParams(page, maxPageSize)));

       
    }
}
