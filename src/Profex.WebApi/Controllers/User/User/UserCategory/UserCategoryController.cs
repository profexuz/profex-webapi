using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Profex.Application.Utils;
using Profex.Persistance.Dtos.Categories;
using Profex.Service.Interfaces.Categories;

namespace Profex.WebApi.Controllers.User.UserCommon.UserCommonCategory
{
    [Route("api/user/category")]
    [ApiController]
    public class UserCategoryController : UserBaseController
    {
        private readonly ICategoryService _service;
        private readonly int maxPageSize = 30;
        public UserCategoryController(ICategoryService Categoryservice)
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

        /*[HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAsync([FromForm] CategoryCreateDto dto)
        {
            //var validator = new CompanyCreateValidator();
            //var result = validator.Validate(dto);
            //if (result.IsValid) return Ok(await _service.CreateAsync(dto));
            return Ok(await _service.CreateAsync(dto));

            //await _service.CreateAsync(dto);
            //else return BadRequest(result.Errors);
        }

        [HttpPut("(catrgoryId)")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(long categoryId, [FromForm] CategoryUpdateDto dto)
        {
            //var validator = new CompanyUpdateValidator();
            //var validationResult = validator.Validate(dto);
            //if (validationResult.IsValid) return Ok(await _service.UpdateAsync(companyId, dto));
            //else return BadRequest(validationResult.Errors);
            return Ok(await _service.UpdateAsync(categoryId, dto));
        }

        [HttpDelete("{categoryId}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(long categoryId)
            => Ok(await _service.DeleteAsync(categoryId));*/
    }
}
