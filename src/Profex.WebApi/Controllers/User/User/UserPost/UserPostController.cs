using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profex.Application.Utils;
using Profex.Persistance.Dtos.Posts;
using Profex.Persistance.Validations.Dtos.Posts;
using Profex.Service.Interfaces.Posts;

namespace Profex.WebApi.Controllers.User.UserCommon.UserCommonPost
{
    [Route("api/user/post")]
    [ApiController]
    public class UserPostController : ControllerBase
    {
        private readonly int maxPageSize = 30;
        private readonly IPostService _service;


        public UserPostController(IPostService Postservice)
        {
            _service = Postservice;
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] PostCreateDto dto)
        {
            var validator = new PostCreateValidator();
            var result = validator.Validate(dto);
            if (result.IsValid) return Ok(await _service.CreateAsync(dto));
            else return BadRequest(result.Errors);
        }

        [HttpPut("{postId}")]
        public async Task<IActionResult> UpdateAsync(long postId, [FromForm] PostUpdateDto dto)
        {
            var validator = new PostUpdateValidator();
            var validationResult = validator.Validate(dto);
            if (validationResult.IsValid) return Ok(await _service.UpdateAsync(postId, dto));
            else return BadRequest(validationResult.Errors);
        }

        [HttpDelete("{categoryId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(long postId)
            => Ok(await _service.DeleteAsync(postId));

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchAsync([FromQuery] string search, [FromQuery] int page = 1)
            => Ok(await _service.SearchAsync(search, new PaginationParams(page, maxPageSize)));
    }
}
