using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profex.Persistance.Dtos.Skills;
using Profex.Persistance.Validations.Dtos.Skills;
using Profex.Service.Interfaces.Skills;

namespace Profex.WebApi.Controllers.Admin.Skill;

[Route("api/admin/skill")]
[ApiController]
public class AdminSkill : ControllerBase
{
    private readonly ISkillService _skillService;

    public AdminSkill(ISkillService skillService )
    {
        _skillService = skillService;
    }


    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateAsync([FromForm] SkillCreateDto dto)
    {
        var validator = new SkillCreateValidator();
        var result = validator.Validate(dto);
        if (result.IsValid) return Ok(await _skillService.CreateAsync(dto));
        
        else return BadRequest(result.Errors);
    }


    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateAsync(long id, [FromForm] SkillUpdateDto dto)
    {
        return Ok(await _skillService.UpdateAsync(id, dto));
    }


    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAsync(long id)
          => Ok(await _skillService.DeleteAsync(id));

}
