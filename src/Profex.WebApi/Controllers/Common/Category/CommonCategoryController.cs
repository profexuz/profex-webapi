using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profex.Application.Utils;
using Profex.Service.Interfaces.Categories;
using Profex.Service.Services.Categories;

namespace Profex.WebApi.Controllers.Common.Category
{
    [Route("api/common/categories")]
    [ApiController]
    public class CommonCategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        private readonly int maxPageSize = 30;
        public CommonCategoryController(ICategoryService Categoryservice)
        {
            _categoryService = Categoryservice;

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _categoryService.GetAllAsync(new PaginationParams(page, maxPageSize)));


        [HttpGet("{categoryId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(long categoryId)
        => Ok(await _categoryService.GetByIdAsync(categoryId));

    
        //[HttpGet("skills/{categoryId}")]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetAllSkillByCategoryId(long categoryId, int page = 1)
        //{
        //    var ps = await _categoryService.GetAllSkillByCategoryId(categoryId, new PaginationParams(page, maxPageSize));

        //    return Ok(ps);
        //}


        //[HttpGet("allSkillsBy/categoryId")]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetAllSkillByCategoryId(long categoryId, int page = 1)
        //{
        //    var ps = await _service.GetAllSkillByCategoryId(categoryId, new PaginationParams(page, maxPageSize));

        //    return Ok(ps);
        //}
    }
}
