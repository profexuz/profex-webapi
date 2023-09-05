using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profex.Application.Utils;
using Profex.Persistance.Dtos.Categories;
using Profex.Persistance.Validations.Dtos.Categories;
using Profex.Service.Interfaces.Categories;

namespace Profex.WebApi.Controllers.Common.Category
{
    [Route("api/common/category")]
    [ApiController]
    public class CommonCategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        private readonly int maxPageSize = 30;
        public CommonCategoryController(ICategoryService Categoryservice)
        {
            _service = Categoryservice;
        }


        [HttpPost]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> CreateAsync([FromForm] CategoryCreateDto dto)
        {
            var validator = new CategoryCreateValidator();
            var result = validator.Validate(dto);
            if (result.IsValid) return Ok(await _service.CreateAsync(dto));
            else return BadRequest(result.Errors);

        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(long categoryId, [FromForm] CategoryUpdateDto dto)
        {
            var updateValidator = new CategoryUpdateValidator();
            var result = updateValidator.Validate(dto);
            if (result.IsValid) return Ok(await _service.UpdateAsync(categoryId, dto));
            else return BadRequest(result.Errors);
        }

        [HttpDelete("{categoryId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(long categoryId)
            => Ok(await _service.DeleteAsync(categoryId));


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

        [HttpGet("{categoryId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(long categoryId)
        => Ok(await _service.GetByIdAsync(categoryId));

        [HttpGet("sort/byCategory")]
        [AllowAnonymous]
        public async Task<ActionResult<IList<Domain.Entities.posts.Post>>> GetPostsByCategory(long category)
        {
            var ps = await _service.GetPostsByCategory(category);
            return Ok(ps);
        }

        [HttpGet("getall/by/categoryId")]
        [AllowAnonymous]
        public async Task<ActionResult<IList<Domain.Entities.skills.Skill>>> GetAllSkillByCategoryId(long  categoryId)
        {
            var ps = await _service.GetAllSkillByCategoryId(categoryId);
            return Ok(ps);
        }
    }
}
