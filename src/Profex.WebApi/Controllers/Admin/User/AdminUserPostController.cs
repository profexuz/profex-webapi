using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profex.Persistance.Dtos.Posts;
using Profex.Persistance.Validations.Dtos.Posts;
using Profex.Service.Interfaces.Posts;

namespace Profex.WebApi.Controllers.Admin.User
{
    [Route("api/admin/user/post")]
    [ApiController]
    public class AdminUserPostController : ControllerBase
    {
        private readonly IPostService _postService;

        public AdminUserPostController(IPostService Postservice)
        {
            _postService = Postservice;
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(long id, [FromForm] PostUpdateDto dto)
        {
            var validator = new PostUpdateValidator();
            var validationResult = validator.Validate(dto);
            if (validationResult.IsValid) return Ok(await _postService.UpdateAsync(id, dto));
            else return BadRequest(validationResult.Errors);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(long id)
            => Ok(await _postService.DeleteAsync(id));

    }
}
