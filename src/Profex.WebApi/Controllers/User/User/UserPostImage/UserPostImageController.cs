using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profex.Application.Utils;
using Profex.Persistance.Dtos.PostImages;
using Profex.Service.Interfaces.PostImages;

namespace Profex.WebApi.Controllers.User.User.UserPostImage
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPostImageController : ControllerBase
    {
        private readonly int maxPageSize = 30;
        //private readonly IPostService _service;
        private readonly IPostImagesService _service;
        public UserPostImageController(IPostImagesService service)
        {
            this._service = service;            
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAsync([FromForm] PostImageCreateDto dto)
        {
            //var validator = new PostImageCreateValidator();
            //var result = validator.Validate(dto);
            //if (result.IsValid) return Ok(await _service.CreateAsync(dto));
            //else return BadRequest(result.Errors);

            return Ok(await _service.CreateAsync(dto));
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));



        [HttpGet("{categoryId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(long categoryId)
        => Ok(await _service.GetByIdAsync(categoryId));

    }
}
