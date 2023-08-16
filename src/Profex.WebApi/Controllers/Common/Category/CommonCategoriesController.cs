using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profex.Application.Utils;
using Profex.Persistance.Dtos.Categories;
using Profex.Service.Interfaces.Categories;

namespace Profex.WebApi.Controllers.Common.Category
{
    [Route("api/common/categories")]
    [ApiController]
    public class CommonCategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;
        private readonly int maxPageSize = 30;
        public CommonCategoriesController(ICategoryService Categoryservice)
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
    }
}
