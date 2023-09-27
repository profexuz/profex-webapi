using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profex.Application.Utils;
using Profex.Persistance.Dtos.PostImages;
using Profex.Persistance.Validations.Dtos.PostImages;
using Profex.Service.Interfaces.PostImages;

namespace Profex.WebApi.Controllers.User.User.UserPostImage
{
    [Route("api/user/posts/images")]
    [ApiController]
    public class UserPostImageController : ControllerBase
    {
        private readonly int maxPageSize = 30;
        private readonly IPostImagesService _service;
        public UserPostImageController(IPostImagesService service)
        {
            this._service = service;            
        }

        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        //     => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

        //[HttpGet("{id}")]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetByIdAsync(long id)
        //    => Ok(await _service.GetByIdAsync(id));
        
        
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateAsync([FromForm] PostImageCreateDto dto)
        {
            var validator = new PostImageValidator();
            var result = validator.Validate(dto);
            if (result.IsValid) return Ok(await _service.CreateAsync(dto));

            else return BadRequest(result.Errors);
        }

        [HttpPut]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateAsync(long id, [FromForm] PostImageCreateDto dto)
        {
            var validator = new PostImageValidator();
            var result = validator.Validate(dto);
            if (result.IsValid) return Ok(await _service.UpdateAsync(id, dto));
                     
            else  return BadRequest(result.Errors);
        }

        [HttpDelete]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteAsync(long id)
          => Ok(await _service.DeleteAsync(id));

    }
}
