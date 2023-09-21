using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profex.Application.Utils;
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));


        [HttpGet("{categoryId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(long categoryId)
        => Ok(await _service.GetByIdAsync(categoryId));


        [HttpGet("allposts/byCategory/{categoryId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPostsByCategory(long categoryId, int page = 1)
        {
            var ps = await _service.GetPostsByCategory(categoryId, new PaginationParams(page, maxPageSize));

            return Ok(ps);
        }


        [HttpGet("allSkillsBy/categoryId")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllSkillByCategoryId(long categoryId, int page = 1)
        {
            var ps = await _service.GetAllSkillByCategoryId(categoryId, new PaginationParams(page, maxPageSize));

            return Ok(ps);
        }
    }
}
