using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Profex.Persistance.Dtos.Categories;
using Profex.Persistance.Validations.Dtos.Categories;
using Profex.Service.Interfaces.Categories;

namespace Profex.WebApi.Controllers.Admin.Categories;

[Route("api/admincontroller")]
[ApiController]
public class AdminCategory : ControllerBase
{
    private readonly ICategoryService _service;
    private readonly int maxPageSize = 30;
    public AdminCategory(ICategoryService Categoryservice)
    {
        _service = Categoryservice;
    }
    
    
    [HttpPost]
    [Authorize(Roles ="Admin")]
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

}
